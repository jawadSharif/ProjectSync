using System;
using System.Collections.Generic;

namespace MyCompany.Managers.Dto
{

    public class AsanaCompletedBy
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaDependency
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
    }

    public class AsanaDependent
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
    }

    public class AsanaExternal
    {
        public string data { get; set; }
        public string gid { get; set; }
    }

    public class AsanaUser
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaHeart
    {
        public string gid { get; set; }
        public AsanaUser user { get; set; }
    }

    public class AsanaLike
    {
        public string gid { get; set; }
        public AsanaUser user { get; set; }
    }

    public class AsanaProject
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaSection
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaMembership
    {
        public AsanaProject project { get; set; }
        public AsanaSection section { get; set; }
    }

    public class AsanaAssignee
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaCreatedBy
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaEnumOption
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string color { get; set; }
        public bool enabled { get; set; }
        public string name { get; set; }
    }

    public class AsanaEnumValue
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string color { get; set; }
        public bool enabled { get; set; }
        public string name { get; set; }
    }

    public class AsanaCustomField
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public AsanaCreatedBy created_by { get; set; }
        public string currency_code { get; set; }
        public string custom_label { get; set; }
        public string custom_label_position { get; set; }
        public string description { get; set; }
        public string display_value { get; set; }
        public bool enabled { get; set; }
        public List<AsanaEnumOption> enum_options { get; set; }
        public AsanaEnumValue enum_value { get; set; }
        public string format { get; set; }
        public bool has_notifications_enabled { get; set; }
        public bool is_global_to_workspace { get; set; }
        public string name { get; set; }
        public double number_value { get; set; }
        public int precision { get; set; }
        public string resource_subtype { get; set; }
        public string text_value { get; set; }
        public string type { get; set; }
    }

    public class AsanaFollower
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaParent
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaProject2
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaTag
    {
        public string gid { get; set; }
        public string name { get; set; }
    }

    public class AsanaWorkspace
    {
        public string gid { get; set; }
        public string resource_type { get; set; }
        public string name { get; set; }
    }

    public class AsanaData
    {
        public string gid { get; set; }

        public string resource_type { get; set; }
        public string name { get; set; }

        
       public bool completed { get; set; }
        
       //public DateTime? completed_at { get; set; }
        
       public AsanaCompletedBy completed_by { get; set; }
        
       public DateTime created_at { get; set; }
        
       public string html_notes { get; set; }
       public List<AsanaMembership> memberships { get; set; }
        
       public DateTime modified_at { get; set; }
        
       public string notes { get; set; }
       public AsanaAssignee assignee { get; set; }
       public AsanaParent parent { get; set; }
       public List<AsanaTag> tags { get; set; }
       

        //public string approval_status { get; set; }
        //public List<AsanaDependency> dependencies { get; set; }
        //public List<AsanaDependent> dependents { get; set; }
        //public DateTime due_at { get; set; }
        //public string due_on { get; set; }
        //public AsanaExternal external { get; set; }
        //public bool hearted { get; set; }
        //public List<AsanaHeart> hearts { get; set; }

        //public bool is_rendered_as_separator { get; set; }
        //public bool liked { get; set; }
        //public List<AsanaLike> likes { get; set; }

        //public int num_hearts { get; set; }
        //public int num_likes { get; set; }
        //public int num_subtasks { get; set; }
        //public string resource_subtype { get; set; }
        //public string start_on { get; set; }

        //public List<AsanaCustomField> custom_fields { get; set; }
        //public List<AsanaFollower> followers { get; set; }

        //public string permalink_url { get; set; }
        //public List<AsanaProject> projects { get; set; }

        //public AsanaWorkspace workspace { get; set; }
    }

    public class AsanaTaskDetail
    {
        public List<AsanaData> data { get; set; }
    }


}
