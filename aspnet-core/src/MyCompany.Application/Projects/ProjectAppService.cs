using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using MyCompany.AsanaProjects.Dto;
using MyCompany.DevOpsProject;
using MyCompany.DevOpsProject.Dto;
using MyCompany.Features;
using MyCompany.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCompany.Projects
{
    public class ProjectAppService : MyCompanyAppServiceBase, IProjectAppService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IAsanaAppService _asanaAppService;
        private readonly IDevOpsAppService _devOpsAppService;

        public ProjectAppService(IRepository<Project, Guid> projectRepository,
                                 IAsanaAppService asanaAppService,
                                 IDevOpsAppService devOpsAppService)
        {
            _projectRepository = projectRepository;
            _asanaAppService = asanaAppService;
            _devOpsAppService = devOpsAppService;
        }

        public async Task<PagedResultDto<ProjectDto>> GetAll(ProjectListOutput input)
        {
            var query = _projectRepository.GetAll().Select(x => new ProjectDto()
            {
                Id = x.Id,
                Workspace = x.Workspace,
                ProjectTitle = x.ProjectTitle,
                TaskTitle = x.TaskTitle,
                TasksDescription = x.TasksDescription,
                AsanaId = x.AsanaId,
                Type = x.Type
            });
            var count = await query.CountAsync();
            var items = await query.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
            return new PagedResultDto<ProjectDto>()
            {
                Items = items,
                TotalCount = count
            };
        }

        public async Task Create(CreateProjectDto input)
        {

            if (input.ShouldCreateAsanaTask)
            {
                await CreateTasksForAsana(input);
            }
            else
            {
                await SaveTaskForDevOps(input);
            }
        }

        private async Task CreateTasksForAsana(CreateProjectDto input)
        {
            var query = _projectRepository.GetAll();
            var devOpsWorkItemsInput = new GetWorkItemsInput()
            {
                Organization = input.DevOpsOrganization,
                Project = input.DevOpsProject,
                Token = input.DevOpsAccessToken,
                Version = "6.0"
            };
            var devOpsTasks = await _devOpsAppService.GetProjectWorkItems(devOpsWorkItemsInput);

            devOpsTasks = devOpsTasks.Where(a => !query.Any(x => x.TaskTitle == a.fields.SystemTitle)).ToList();

            var asanaCreateTaskDtoRoot = new CreateTaskDtoRoot()
            {
                Token = input.AsanaToken,
            };
            asanaCreateTaskDtoRoot.task = new CreateTaskDto();
            var asanaTasks = new CreateTaskDtoInput();
            var project = new ProjectDto();

            foreach (var a in devOpsTasks)
            {
                project.Type = "Asana";
                project.ProjectTitle = input.AsanaProjectName;
                project.TaskTitle = asanaTasks.name = a.fields.SystemTitle;
                project.TasksDescription = asanaTasks.notes = StripHTML(a.fields.SystemDescription);
                project.Workspace = asanaTasks.workspace = input.AsanaWorkSpace;
                asanaTasks.projects = new List<string>() { input.AsanaProjectId };
                asanaCreateTaskDtoRoot.task.data = asanaTasks;

                await SaveProjectInDb(project);
                await _asanaAppService.CreateTaskInProject(asanaCreateTaskDtoRoot);

            }

        }

        private async Task SaveTaskForDevOps(CreateProjectDto input)
        {
            var query = _projectRepository.GetAll();

            var asanaTaskInput = new AsanaTaskInput()
            {
                ProjectId = input.AsanaProjectId,
                Token = input.AsanaToken
            };

            var asanaTasks = await _asanaAppService.GetAllTasksInProject(asanaTaskInput);
            asanaTasks = asanaTasks.Where(a => !query.Any(x => x.TaskTitle == a.Value)).ToList();

            foreach (var item in asanaTasks)
            {
                var createDevOpsTaskDto = new CreateDevOpsTaskDto()
                {
                    Version = "6.0",
                    Organization = input.DevOpsOrganization,
                    Project = input.DevOpsProject,
                    Token = input.DevOpsAccessToken,
                    data = new List<CreateDevOpsTaskBodyDto>()
                    {
                        new CreateDevOpsTaskBodyDto()
                        { from = null,
                          op="add",
                          path="/fields/System.Title",
                          value = item.Value
                        }
                    }
                };
                await _devOpsAppService.CreateTask(createDevOpsTaskDto);

                var project = new ProjectDto()
                {
                    ProjectTitle = input.DevOpsProject,
                    TaskTitle = item.Value,
                    Type = "DevOps",
                    Workspace = input.DevOpsOrganization,
                };

                await SaveProjectInDb(project);
            }
        }

        private async Task SaveProjectInDb(ProjectDto input)
        {
            var project = new Project()
            {
                TaskTitle = input.TaskTitle,
                TasksDescription = input.TasksDescription,
                Type = input.Type,
                Workspace = input.Workspace,
                ProjectTitle = input.ProjectTitle,
                AsanaId = input.AsanaId ?? string.Empty,
                DevOpsId = input.DevOpsId,
            };
            await _projectRepository.InsertAsync(project);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        public static string StripHTML(string input)
        {
            return input.IsNullOrEmpty() ? string.Empty : Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}
