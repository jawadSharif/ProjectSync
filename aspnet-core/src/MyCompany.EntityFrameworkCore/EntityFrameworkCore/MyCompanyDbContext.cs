using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using MyCompany.Authorization.Roles;
using MyCompany.Authorization.Users;
using MyCompany.MultiTenancy;
using MyCompany.Projects;
using MyCompany.Tasks;

namespace MyCompany.EntityFrameworkCore
{
    public class MyCompanyDbContext : AbpZeroDbContext<Tenant, Role, User, MyCompanyDbContext>
    {

        
        public MyCompanyDbContext(DbContextOptions<MyCompanyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }


    }
}
