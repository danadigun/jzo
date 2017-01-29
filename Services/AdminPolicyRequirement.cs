using jzo.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace jzo.Services
{
    public class CanManageStoreRequirement : IAuthorizationRequirement
    {
        public string AcceptedRole;

        //requirement here
        public CanManageStoreRequirement(string typeOfuser)
        {
            AcceptedRole = typeOfuser;
        }
    }

    public class CanManageStoreHandler : AuthorizationHandler<CanManageStoreRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanManageStoreRequirement requirement)
        {
            //get base url
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                // .NET 4.x -> return Task.FromResult(0);
                return Task.CompletedTask;
            }

            var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            if(role == requirement.AcceptedRole)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }


    }
}
