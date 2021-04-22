using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace MyCompany.Controllers
{
    public abstract class MyCompanyControllerBase: AbpController
    {
        protected MyCompanyControllerBase()
        {
            LocalizationSourceName = MyCompanyConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
