using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Managers.Dto
{
    public class BaseDevOps<T> where T: class
    {
        public int count { get; set; }
        public List<T> value { get; set; }
    }

    public class DevOpsProjectList
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string state { get; set; }
        public int revision { get; set; }
        public string visibility { get; set; }
        public DateTime lastUpdateTime { get; set; }
    }
}
