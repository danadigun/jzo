using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using jzo.Models;
using jzo.Services;

namespace jzo.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //Application db context controller
        public ApplicationDbContext()
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<SelectedItems> SelectedItem { get; set; }
        public DbSet<ItemGroup> ItemGroup { get; set; }
        public DbSet<Item>Item { get; set; }
        public DbSet<Checkout> Checkout { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<CustomOrder> CustomOrder { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
