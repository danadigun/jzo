using jzo.Models;
using jzo.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;


namespace jzo.Data
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }



        public DbSet<Order> Order { get; set; }
        public DbSet<SelectedItems> SelectedItem { get; set; }
        public DbSet<ItemGroup> ItemGroup { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Checkout> Checkout { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<CustomOrder> CustomOrder { get; set; }

        //protected override void OnModelCreating(ModuleBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    // Customize the ASP.NET Identity model and override the defaults if needed.
        //    // For example, you can rename the ASP.NET Identity table names and more.
        //    // Add your customizations after calling base.OnModelCreating(builder);
        //}
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
