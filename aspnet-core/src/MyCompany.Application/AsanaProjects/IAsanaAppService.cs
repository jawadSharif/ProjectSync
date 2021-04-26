﻿using MyCompany.Managers.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.AsanaProjects
{
    public interface IAsanaAppService
    {
        Task<List<KeyValuePair<string, string>>> GetAsanaWorkSpaces(BasicToken input);

        Task<List<KeyValuePair<string, string>>> GetAllProjectInWorkSpace(ProjectInput input);

        Task<AsanaTaskDetail> GetAllTasksInProject(AsanaTaskInput input);

        Task CreateTaskInProject(CreateTaskDtoRoot input);
    }
}
