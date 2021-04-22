using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject.Dto
{
    public class GetProjectsInput
    {
        public string Organization { get; set; }
        public string Token { get; set; }
        public string Version { get; set; }
    }
}
