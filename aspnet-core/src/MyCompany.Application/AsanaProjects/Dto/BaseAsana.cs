using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.AsanaProjects.Dto
{
    public class BaseAsana<T> where T : class
    {
        public List<T> data { get; set; }
    }

    public class AsanaWorkspace
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaProject
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaProjectTasks
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }
}
