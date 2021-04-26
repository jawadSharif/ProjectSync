using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Managers.Dto
{
    public class ProjectListInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
