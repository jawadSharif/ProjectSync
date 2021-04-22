using MyCompany.AsanaProjects.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects
{
    public interface IAsanaAppService
    {
        Task<List<KeyValuePair<string, string>>> GetAsanaWorkSpaces(BasicToken input);

        Task<List<KeyValuePair<string, string>>> GetAllProjectInWorkSpace(ProjectInput input);

        Task<List<KeyValuePair<string, string>>> GetAllTasksInProject(AsanaTaskInput input);

        Task CreateTaskInProject(CreateTaskDtoRoot input);
    }
}
