using Abp.Domain.Services;
using MyCompany.DevOpsProject.Dto;
using MyCompany.Managers.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.Managers
{
    public interface IDevOpsManager : IDomainService
    {
        Tuple<string> AccessToken(string input);
        Task<List<KeyValuePair<string, string>>> GetProjects(GetProjectsInput input);
        Task<List<DevOpsValue>> GetProjectWorkItems(GetWorkItemsInput input);
        Task CreateTask(CreateDevOpsTaskDto input);
    }
}
