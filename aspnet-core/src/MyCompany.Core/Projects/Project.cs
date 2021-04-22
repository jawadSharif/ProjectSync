using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.Projects
{
    [Table("Projects")]
    public class Project : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string Workspace { get; set; }
        public string ProjectTitle { get; set; }
        public string TaskTitle { get; set; }
        public string TasksDescription { get; set; }
        public string Type { get; set; }
        public string AsanaId { get; set; }
        public int? DevOpsId { get; set; }
    }
}
