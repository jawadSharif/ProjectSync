using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MyCompany.AsanaProjects;
using MyCompany.Managers.Dto;
using MyCompany.DevOpsProject;
using MyCompany.Projects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<PagedResultDto<ProjectListOutput>> GetAll(ProjectListInput input)
        {
            var query = _projectRepository.GetAll();
            var projectListQuery = _projectRepository.GetAll().Select(x => new ProjectListOutput()
            {
                Id = x.Id,
                AsanaWorkspace = x.AsanaWorkspace,
                AsanaProject = x.AsanaProject,
                AsanaProjectId = x.AsanaProjectId,
                AsanaToken = x.AsanaToken,
                AsanaWorkSpaceId = x.AsanaWorkSpaceId,
                DevOpsOrganization = x.DevOpsOrganization,
                DevOpsProjectTitle = x.DevOpsProjectTitle,
                DevOpsToken = x.DevOpsToken,
                UpdateAsana = x.UpdateAsana,
                UpdateDevOps = x.UpdateDevOps
            });

            var count = await projectListQuery.CountAsync();
            var items = await projectListQuery.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();

            return new PagedResultDto<ProjectListOutput>()
            {
                Items = items,
                TotalCount = count
            };
        }

        public async Task CreateOrEditProject(ProjectDto input)
        {
            if (input.Id.HasValue)
            {
                await UpdateAsync(input);
            }
            else
            {
                await CreateAsync(input);
            }
        }

        private async Task CreateAsync(ProjectDto input)
        {
            var project = ObjectMapper.Map<Project>(input);
            await _projectRepository.InsertAsync(project);
        }

        private async Task UpdateAsync(ProjectDto input)
        {
            var project = ObjectMapper.Map<Project>(input);
            await _projectRepository.UpdateAsync(project);
        }


        public async Task Create(CreateProjectDto input)
        {

            if (input.ShouldCreateAsanaTask)
            {
                await CreateTasksForAsana(input);
            }
            else
            {
                //await SaveTaskForDevOps(input);
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

            //devOpsTasks = devOpsTasks.Where(a => !query.Any(x => x.TaskTitle == a.fields.SystemTitle)).ToList();

            var asanaCreateTaskDtoRoot = new CreateTaskDtoRoot()
            {
                Token = input.AsanaToken,
            };
            asanaCreateTaskDtoRoot.task = new CreateTaskDto();
            var asanaTasks = new CreateTaskDtoInput();
            var project = new ProjectDto();

            foreach (var a in devOpsTasks)
            {
                //project.Type = "Asana";
                //project.ProjectTitle = input.AsanaProjectName;
                //project.TaskTitle = 
                //project.TasksDescription = 
                //project.Workspace = 
                asanaTasks.name = a.fields.SystemTitle;
                //asanaTasks.notes = StripHTML(a.fields.SystemDescription);
                asanaTasks.workspace = input.AsanaWorkSpace;
                asanaTasks.projects = new List<string>() { input.AsanaProjectId };
                asanaCreateTaskDtoRoot.task.data = asanaTasks;

                //await SaveProjectInDb(project);
                await _asanaAppService.CreateTaskInProject(asanaCreateTaskDtoRoot);

            }

        }

        /*
        private async Task SaveTaskForDevOps(CreateProjectDto input)
        {
            var query = _projectRepository.GetAll();

            var asanaTaskInput = new AsanaTaskInput()
            {
                ProjectId = input.AsanaProjectId,
                Token = input.AsanaToken
            };

            var asanaTasks = await _asanaAppService.GetAllTasksInProject(asanaTaskInput);
            //asanaTasks = asanaTasks.Where(a => !query.Any(x => x.TaskTitle == a.Value)).ToList();

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
                    //ProjectTitle = input.DevOpsProject,
                    //TaskTitle = item.Value,
                    //Type = "DevOps",
                    //Workspace = input.DevOpsOrganization,
                };

                await SaveProjectInDb(project);
            }
        }

        private async Task SaveProjectInDb(ProjectDto input)
        {
            //var project = new Project()
            //{
            //    TaskTitle = input.TaskTitle,
            //    TasksDescription = input.TasksDescription,
            //    Type = input.Type,
            //    Workspace = input.Workspace,
            //    ProjectTitle = input.ProjectTitle,
            //    AsanaId = input.AsanaId ?? string.Empty,
            //    DevOpsId = input.DevOpsId,
            //};
            //await _projectRepository.InsertAsync(project);
            //await CurrentUnitOfWork.SaveChangesAsync();
        }


        public static string StripHTML(string input)
        {
            return input.IsNullOrEmpty() ? string.Empty : Regex.Replace(input, "<.*?>", String.Empty);
        }
        */
    }
}
