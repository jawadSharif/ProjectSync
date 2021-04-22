using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.AsanaProjects.Dto
{
    public class CreateTaskDtoRoot : BasicToken
    {
        public CreateTaskDto task { get; set; }
    }

    public class CreateTaskDto
    {
        public CreateTaskDtoInput data { get; set; }
    }

    public class CreateTaskDtoInput
    {
        public string name { get; set; }
        public string notes { get; set; }
        public List<string> projects { get; set; }
        public string workspace { get; set; }
    }

   


}
