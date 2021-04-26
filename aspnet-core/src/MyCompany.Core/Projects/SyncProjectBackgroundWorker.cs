using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Threading;
using Abp.Threading.BackgroundWorkers;
using Abp.Threading.Timers;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;
using MyCompany.Managers;
using MyCompany.Managers.Dto;
using MyCompany.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects
{
    public class SyncProjectBackgroundWorker : PeriodicBackgroundWorkerBase, ISingletonDependency
    {
        private readonly IRepository<ProjectTask, Guid> _projectTaskRepository;
        private readonly IRepository<Project, Guid> _projectRepository;
        private IAsanaManager _asanaManager;
        private readonly IDevOpsManager _devOpsManager;

        public SyncProjectBackgroundWorker(AbpTimer timer,
                                            IRepository<ProjectTask, Guid> projectTaskRepository,
                                            IRepository<Project, Guid> projectRepository,
                                            IAsanaManager asanaManager,
                                            IDevOpsManager devOpsManager)
       : base(timer)
        {
            _projectTaskRepository = projectTaskRepository;
            _projectRepository = projectRepository;
            _asanaManager = asanaManager;
            _devOpsManager = devOpsManager;
            Timer.Period = 300000; //5 mints (good for tests, but normally will be more)
        }

        [UnitOfWork]
        protected override void DoWork()
        {
            var projects = AsyncHelper.RunSync(() => _projectRepository.GetAll().ToListAsync());
            foreach(var project in projects)
            {
                var utcNow = Clock.Now.ToUniversalTime();
                var asanaBaseToken = new AsanaTaskInput()
                {
                    Token = project.AsanaToken,
                    ProjectId = project.AsanaProjectId,
                };

                var asanaTasks = AsyncHelper.RunSync(() => _asanaManager.GetAllTasksInProject(asanaBaseToken));


                var getWorkItemsInput = new GetWorkItemsInput()
                {
                    Organization = project.DevOpsOrganization,
                    Project = project.DevOpsProjectTitle,
                    Token = project.DevOpsToken,
                    Version = "6.0"
                };

                var result = AsyncHelper.RunSync(() => _devOpsManager.GetProjectWorkItems(getWorkItemsInput));
                AsyncHelper.RunSync(() => UpsertAsanaTasks(asanaTasks, project));
                AsyncHelper.RunSync(() => UpsertDevOpsTasks(result, project));

            }
        }


        public async Task UpsertAsanaTasks(AsanaTaskDetail asanaTaskDetail, Project project)
        {
            foreach (var item in asanaTaskDetail.data)
            {
                var projectTask = await _projectTaskRepository.FirstOrDefaultAsync(a => a.AsanaId == item.gid);
                if (projectTask == null)
                {
                    projectTask = new ProjectTask()
                    {
                        AsanaId = item.gid,
                        TaskTitle = item.name,
                        TasksDescription = item.html_notes,
                        ProjectId = project.Id,
                        Status = item.completed ? "Completed" : "",
                    };
                    await _projectTaskRepository.InsertAsync(projectTask);
                }
                else
                {
                    if (item.modified_at > projectTask.LastModificationTime)
                    {
                        projectTask.AsanaId = item.gid;
                        projectTask.TaskTitle = item.name;
                        projectTask.TasksDescription = item.html_notes;
                        projectTask.Status = item.completed ? "Completed" : "";
                        await _projectTaskRepository.UpdateAsync(projectTask);
                    }
                }
            }
        }

        public async Task UpsertDevOpsTasks(List<DevOpsValue> devOpsTasks, Project project)
        {
            foreach (var item in devOpsTasks)
            {
                var projectTask = await _projectTaskRepository.FirstOrDefaultAsync(a => a.DevOpsId == item.id);
                if (projectTask == null)
                {
                    projectTask = new ProjectTask()
                    {
                        DevOpsId = item.id,
                        TaskTitle = item.fields.SystemTitle,
                        TasksDescription = item.fields.SystemDescription,
                        ProjectId = project.Id,
                    };
                    await _projectTaskRepository.InsertAsync(projectTask);
                }
                else
                {
                    if (item.fields.SystemChangedDate > projectTask.LastModificationTime)
                    {
                        projectTask.TaskTitle = item.fields.SystemTitle;
                        projectTask.TasksDescription = item.fields.SystemDescription;
                        //projectTask.Status = item.completed ? "Completed" : "";
                        await _projectTaskRepository.UpdateAsync(projectTask);
                    }
                }
            }
        }


    }
}
