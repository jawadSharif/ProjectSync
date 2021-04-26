using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Managers.Dto
{
    #region DevOpsWorkItem
    public class DevOpsAvatar
    {
        public string href { get; set; }
    }

    public class DevOpsLinks
    {
        public DevOpsAvatar avatar { get; set; }
    }

    public class DevOpsSystemCreatedBy
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public DevOpsLinks _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class DevOpsSystemChangedBy
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public DevOpsLinks _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class DevOpsSystemAssignedTo
    {
        public string displayName { get; set; }
        public string url { get; set; }
        public DevOpsLinks _links { get; set; }
        public string id { get; set; }
        public string uniqueName { get; set; }
        public string imageUrl { get; set; }
        public string descriptor { get; set; }
    }

    public class DevOpsFields
    {
        [JsonProperty("System.AreaPath")]
        public string SystemAreaPath { get; set; }

        [JsonProperty("System.TeamProject")]
        public string SystemTeamProject { get; set; }

        [JsonProperty("System.IterationPath")]
        public string SystemIterationPath { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string SystemWorkItemType { get; set; }

        [JsonProperty("System.State")]
        public string SystemState { get; set; }

        [JsonProperty("System.Reason")]
        public string SystemReason { get; set; }

        [JsonProperty("System.CreatedDate")]
        public DateTime SystemCreatedDate { get; set; }

        [JsonProperty("System.CreatedBy")]
        public DevOpsSystemCreatedBy SystemCreatedBy { get; set; }

        [JsonProperty("System.ChangedDate")]
        public DateTime SystemChangedDate { get; set; }

        [JsonProperty("System.ChangedBy")]
        public DevOpsSystemChangedBy SystemChangedBy { get; set; }

        [JsonProperty("System.Title")]
        public string SystemTitle { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.Effort")]
        public int MicrosoftVSTSSchedulingEffort { get; set; }

        [JsonProperty("WEF_6CB513B6E70E43499D9FC94E5BBFB784_Kanban.Column")]
        public string WEF6CB513B6E70E43499D9FC94E5BBFB784KanbanColumn { get; set; }

        [JsonProperty("System.Description")]
        public string SystemDescription { get; set; }

        [JsonProperty("System.AssignedTo")]
        public DevOpsSystemAssignedTo SystemAssignedTo { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.RemainingWork")]
        public int? MicrosoftVSTSSchedulingRemainingWork { get; set; }

        [JsonProperty("System.Tags")]
        public string SystemTags { get; set; }
    }

    public class DevOpsValue
    {
        public int id { get; set; }
        public int rev { get; set; }
        public DevOpsFields fields { get; set; }
        public string url { get; set; }
    }

    public class DevOpsWorkItem
    {
        public int count { get; set; }
        public List<DevOpsValue> value { get; set; }
    }

    #endregion

    public class DevopsWorkItemQuery
    {
        public string query { get; set; }
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
}
