using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyCompany.EntityFrameworkCore;
using MyCompany.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace MyCompany.Web.Tests
{
    [DependsOn(
        typeof(MyCompanyWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class MyCompanyWebTestModule : AbpModule
    {
        public MyCompanyWebTestModule(MyCompanyEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(MyCompanyWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(MyCompanyWebMvcModule).Assembly);
        }
    }
}