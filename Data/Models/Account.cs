using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? Idcustomer { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }

        public virtual Customer IdcustomerNavigation { get; set; }
    }
}
