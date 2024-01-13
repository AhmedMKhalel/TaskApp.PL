// ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace TaskApp.DAL.Entities
{

    public class ApplicationUser : IdentityUser
    {
        public string OTP { get; set; }
    }
}
