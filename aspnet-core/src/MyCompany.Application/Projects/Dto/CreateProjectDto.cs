using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects.Dto
{
    public class CreateProjectDto
    {
        public string DevOpsAccessToken { get; set; }
        public string DevOpsOrganization { get; set; }
        public string DevOpsProject { get; set; }

        public string AsanaToken { get; set; }
        public string AsanaWorkSpace { get; set; }
        public string AsanaProjectId { get; set; }
        public string AsanaProjectName { get; set; }
        public bool ShouldCreateAsanaTask { get; set; }
    }
}
