using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskApp.DAL.Entities;

namespace TaskApp.DAL.Contexts
{

    // ApplicationDbContext.cs
    public class MvcAppDbContext : IdentityDbContext<IdentityUser>
    {
        public MvcAppDbContext(DbContextOptions<MvcAppDbContext> options) : base(options)
        {


        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Additional configurations if needed
        //}
    }
}

