using LinkDev.Talabat.Core.Domain.Contract.Persistance.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Common;
using LinkDev.Talabat.Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace LinkDev.Talabat.Infrastructure.Persistence.Identity
{
    public class StoreIdentityDbInitializer(StoreIdentityDbContext _storeIdentityDbContext, UserManager<ApplicationUser> userManager) 
        : DbInitializer(_storeIdentityDbContext), IStoreIdentityDbInitializer
    {
        public override async Task SeedAsync()
        {
            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Mohamed Yasser",
                    UserName = "Mohamed.Yasser",
                    Email = "MohamedYasser@gmail",
                    PhoneNumber = "1234567890",

                };
                await userManager.CreateAsync(user, "P@ssw0rd"); 
            }
        }
    }
}
