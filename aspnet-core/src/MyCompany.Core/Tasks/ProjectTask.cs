using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using MyCompany.Projects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany.Tasks
{
    [Table("ProjectTasks")]
    public class ProjectTask : FullAuditedEntity<Guid>, IMayHaveTenant
    {
        public int? TenantId { get; set; }
        public string TaskTitle { get; set; }
        public string Tags { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        public string TasksDescription { get; set; }
        public string AsanaId { get; set; }
        public int? DevOpsId { get; set; }

        public Guid ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project ProjectFk { get; set; }
    }
}
