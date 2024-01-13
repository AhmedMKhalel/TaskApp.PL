using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskApp.DAL.Entities;

namespace TaskApp.DAL.Contexts
{
    // ApplicationDbContext.cs
    public class MvcAppDbContext : IdentityDbContext<ApplicationUser>
    {
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
