
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using TaskApp.DAL.Entities;

namespace TaskApp.PL.Helper
{


    public static class IdentityConfig
    {
        public static async Task SeedBasicUser(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser()
            {
                UserName = "ahmed.netdeveloper6@gmail.com",
                Email = "ahmed.netdeveloper6@gmail.com",
                EmailConfirmed = true,
            };
            var user =await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {

              await userManager.CreateAsync(defaultUser, "User300#");
            }
        }
    }
}
