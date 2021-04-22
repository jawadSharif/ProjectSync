using MyCompany.DevOpsProject.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject
{
    public interface IDevOpsAppService
    {
        Task<Tuple<string>> AccessToken(string input);
        Task<List<KeyValuePair<string, string>>> GetProjects(GetProjectsInput input);
        Task<List<WorkItemDetail>> GetProjectWorkItems(GetWorkItemsInput input);

        Task CreateTask(CreateDevOpsTaskDto input);
    }
}
