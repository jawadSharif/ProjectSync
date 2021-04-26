using MyCompany.DevOpsProject.Dto;
using MyCompany.Managers;
using MyCompany.Managers.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject
{
    public class DevOpsAppService : MyCompanyAppServiceBase, IDevOpsAppService
    {
        private readonly IDevOpsManager _devOpsManager;

        public DevOpsAppService(IDevOpsManager devOpsManager)
        {
            _devOpsManager = devOpsManager;
        }
        public Tuple<string> AccessToken(string input)
        {
            return _devOpsManager.AccessToken(input);
        }

        public async Task<List<KeyValuePair<string, string>>> GetProjects(GetProjectsInput input)
        {
            return await _devOpsManager.GetProjects(input);
        }

        public async Task<List<DevOpsValue>> GetProjectWorkItems(GetWorkItemsInput input)
        {
            return await _devOpsManager.GetProjectWorkItems(input);
        }

        public async Task CreateTask(CreateDevOpsTaskDto input)
        {
            await _devOpsManager.CreateTask(input);
        }


    }
}
