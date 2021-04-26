﻿using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Projects.Dto
{
    public class ProjectDto : NullableIdDto<Guid>
    {
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
