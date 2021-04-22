using Abp.Authorization;
using MyCompany.Authorization.Roles;
using MyCompany.Authorization.Users;

namespace MyCompany.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
