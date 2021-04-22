using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects.Dto
{
   public class ProjectDto :EntityDto<Guid>
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
