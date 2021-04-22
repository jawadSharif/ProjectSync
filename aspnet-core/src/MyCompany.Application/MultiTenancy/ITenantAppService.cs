using Abp.Application.Services;
using MyCompany.MultiTenancy.Dto;

namespace MyCompany.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

