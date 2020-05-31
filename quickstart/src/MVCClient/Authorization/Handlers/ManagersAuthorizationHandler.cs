using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCClient.Authorization.Handlers
{
    public class ManagersAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Bakery>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Bakery resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            //if (requirement.Name != Constants.ReadOperationName &&
            //    requirement.Name != Constants.ApproveOperationName &&
            //    requirement.Name != Constants.RejectOperationName)
            //{
            //    return Task.CompletedTask;
            //}

            if (context.User.IsInRole(Constants.ManagersRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
