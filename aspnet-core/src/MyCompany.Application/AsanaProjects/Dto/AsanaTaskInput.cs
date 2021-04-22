using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.AsanaProjects.Dto
{
    public class AsanaTaskInput : BasicToken
    {
        public string ProjectId { get; set; }
    }
}
