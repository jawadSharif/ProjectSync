using System.Threading.Tasks;
using Abp.Application.Services;
using MyCompany.Authorization.Accounts.Dto;

namespace MyCompany.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
