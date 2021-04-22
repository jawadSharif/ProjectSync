using System.Threading.Tasks;
using Abp.Application.Services;
using MyCompany.Sessions.Dto;

namespace MyCompany.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
