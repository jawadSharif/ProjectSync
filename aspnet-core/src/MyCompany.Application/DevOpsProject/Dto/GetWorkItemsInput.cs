using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject.Dto
{
    public class GetWorkItemsInput
    {
        public string Organization { get; set; }
        public string Project { get; set; }

        public string Token { get; set; }

        public string Version { get; set; }
    }
}
