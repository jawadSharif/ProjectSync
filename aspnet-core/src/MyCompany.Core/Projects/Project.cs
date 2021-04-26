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
        public string AsanaToken { get; set; }
        public string AsanaWorkspace { get; set; }
        public string AsanaWorkSpaceId { get; set; }
        public string AsanaProject { get; set; }
        public string AsanaProjectId { get; set; }
        public string DevOpsToken { get; set; }
        public string DevOpsOrganization { get; set; }
        public string DevOpsProjectTitle { get; set; }
        public string DevOpsProjectId { get; set; }
        public bool UpdateAsana { get; set; }
        public bool UpdateDevOps { get; set; }
    }
}
