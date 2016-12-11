using jzo.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class AdminPolicyRequirement : IAuthorizationRequirement
    {
        //requirement here
    }

    public class AdminPolicyHandler : AuthorizationHandler<AdminPolicyRequirement>
    {
        
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminPolicyRequirement requirement)
        {
            using(var db = new ApplicationDbContext())
            {
                var isAdmin = db.Admins.Where(x => x.email == context.User.Identity.Name).SingleOrDefault();
                if(isAdmin != null)
                {
                    context.Succeed(requirement);
                }
                return Task.CompletedTask;
            }
        }
    }
}
