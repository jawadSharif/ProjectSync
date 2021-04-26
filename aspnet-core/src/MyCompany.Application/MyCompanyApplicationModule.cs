using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using MyCompany.Authorization;
using MyCompany.Projects;
using MyCompany.Projects.Dto;

namespace MyCompany
{
    [DependsOn(
        typeof(MyCompanyCoreModule),
        typeof(AbpAutoMapperModule))]
    public class MyCompanyApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<MyCompanyAuthorizationProvider>();
            Configuration.Modules.AbpAutoMapper().Configurators.Add(config =>
            {
                config.CreateMap<ProjectDto, Project>().ReverseMap();
            });
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(MyCompanyApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
