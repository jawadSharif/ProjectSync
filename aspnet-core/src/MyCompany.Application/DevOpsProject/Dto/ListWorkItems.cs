using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.DevOpsProject.Dto
{
    public class WorkItemFieldReference
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Field
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class WorkItemQuerySortColumn
    {
        public Field field { get; set; }
        public bool descending { get; set; }
    }

    public class ListWorkItems
    {
        public int id { get; set; }
        public string url { get; set; }
    }


    public class WorkItemRoot
    {
        //public string queryType { get; set; }
        //public string queryResultType { get; set; }
        //public DateTime asOf { get; set; }
        //public List<WorkItemFieldReference> columns { get; set; }
        //public List<WorkItemQuerySortColumn> sortColumns { get; set; }
        public List<ListWorkItems> workItems { get; set; }
    }

    public class WorkItemFields
    {
        [JsonProperty("System.Title")]
        public string SystemTitle { get; set; }

        [JsonProperty("System.Description")]
        public string SystemDescription { get; set; }
    }

    public class WorkItemDetail
    {
        public int id { get; set; }
        public WorkItemFields fields { get; set; }
        public string url { get; set; }
    }

    public class WorkItemDetailRoot
    {
        public int count { get; set; }
        public List<WorkItemDetail> value { get; set; }
    }

    public class WorkItemQuery
    {
        public string query { get; set; }
    }
}
