using System.Threading.Tasks;
using MyCompany.Configuration.Dto;

namespace MyCompany.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
