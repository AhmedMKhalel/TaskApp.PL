
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TaskApp.PL.Helper
{


    public static class IdentityConfig
    {
        public static async Task SeedBasicUser(UserManager<IdentityUser> userManager)
        {
            var defaultUser = new IdentityUser()
            {
                UserName = "BasicUser@domain.com",
                Email = "BasicUser@domain.com",
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
