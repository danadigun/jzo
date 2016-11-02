using jzo.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Models
{
    /// <summary>
    /// NOTE: this class' method should be called only once to seed data.
    ///       afterwhich is begins to fail
    /// </summary>
    public class InitializeUsers
    {
        private ApplicationDbContext _context;

        public InitializeUsers(ApplicationDbContext context)
        {
            _context = context;
        }

        public async void SeedAdminUser()
        {
            var user = new ApplicationUser
            {
                UserName = "daniel.adigun@digitalforte.ng",
                NormalizedUserName = "daniel.adigun@digitalforte.ng",
                Email = "daniel.adigun@digitalforte.ng",
                NormalizedEmail = "daniel.adigun@digitalforte.ng",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }

            await _context.SaveChangesAsync();
        }
        public async void SeedCustomer()
        {
            var user = new ApplicationUser
            {
                Email = "debbie.adigun@digitalforte.ng",
                UserName = "debbie.adigun@digitalforte.ng",
                NormalizedEmail = "debbie.adigun@digitalforte.ng",
                NormalizedUserName = "debbie.adigun@digitalforte.ng",
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                EmailConfirmed = true
            };

            var roleStore = new RoleStore<IdentityRole>(_context);

            if (!_context.Roles.Any(r => r.Name == "customer"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "customer", NormalizedName = "customer" });
            }

            if (!_context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "password");
                user.PasswordHash = hashed;
                var userStore = new UserStore<ApplicationUser>(_context);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "customer");
            }

            await _context.SaveChangesAsync();
        }
    }
}
