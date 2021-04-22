using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace MyCompany.EntityFrameworkCore
{
    public static class MyCompanyDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<MyCompanyDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<MyCompanyDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
