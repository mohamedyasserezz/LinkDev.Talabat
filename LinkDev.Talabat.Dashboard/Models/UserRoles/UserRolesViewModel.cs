using LinkDev.Talabat.Dashboard.Models.Roles;

namespace LinkDev.Talabat.Dashboard.Models.UserRoles
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }
}
