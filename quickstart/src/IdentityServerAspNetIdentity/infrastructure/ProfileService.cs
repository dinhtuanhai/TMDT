using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.infrastructure
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);
            var roles = await _userManager.GetRolesAsync(user);

            IList<Claim> userclaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>();
            foreach (var claim in userclaims)
            {
                string type = claim.Type;
                string value = claim.Value;
                claims.Add(new Claim(type, value));
            }

            //var claims = new List<Claim>
            //{
            //    new Claim("name", user.UserName),
            //};
            foreach (var role in roles)
            {
                claims.Add(new Claim("role", role));
            }

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.GetUserAsync(context.Subject);

            context.IsActive = (user != null) &&
                (!user.LockoutEnd.HasValue || user.LockoutEnd.Value <= DateTime.Now);
        }
    }
}
