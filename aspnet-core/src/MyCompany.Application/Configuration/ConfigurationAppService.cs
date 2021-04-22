using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using MyCompany.Configuration.Dto;

namespace MyCompany.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : MyCompanyAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
