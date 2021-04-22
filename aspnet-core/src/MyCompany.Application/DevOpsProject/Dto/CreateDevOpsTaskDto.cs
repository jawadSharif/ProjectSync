using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject.Dto
{
    public class CreateDevOpsTaskDto
    {
        public string Organization { get; set; }
        public string Project { get; set; }
        public string Version { get; set; }
        public string Token { get; set; }

        public List<CreateDevOpsTaskBodyDto> data { get; set; }
    }

    public class CreateDevOpsTaskBodyDto
    {
        public string op { get; set; }
        public string path { get; set; }
        public string from { get; set; }
        public string value { get; set; }
    }
}
