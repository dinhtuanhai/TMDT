using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerAspNetIdentity.Models
{
    public class ListUserViewModel
    {
        public IEnumerable<ApplicationUser> listUserInRoleManager { get; set; }
        public IEnumerable<ApplicationUser> listUserInRoleUser { get; set; }
    }
}
