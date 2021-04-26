using MyCompany.Managers;
using MyCompany.Managers.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyCompany.AsanaProjects
{
    public class AsanaAppService : MyCompanyAppServiceBase, IAsanaAppService
    {
        private readonly IAsanaManager _asanaManager;

        public AsanaAppService(IAsanaManager asanaManager)
        {
            _asanaManager = asanaManager;
        }
        public async Task<List<KeyValuePair<string, string>>> GetAsanaWorkSpaces(BasicToken input)
        {
            return await _asanaManager.GetAsanaWorkSpaces(input);
        }

        public async Task<List<KeyValuePair<string, string>>> GetAllProjectInWorkSpace(ProjectInput input)
        {
            return await _asanaManager.GetAllProjectInWorkSpace(input);
        }

        public async Task<AsanaTaskDetail> GetAllTasksInProject(AsanaTaskInput input)
        {
            var result =  await _asanaManager.GetAllTasksInProject(input);
            return result;
        }

        public async Task CreateTaskInProject(CreateTaskDtoRoot input)
        {
            await _asanaManager.CreateTaskInProject(input);
        }

    }
}
