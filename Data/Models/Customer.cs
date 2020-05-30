using System;
using System.Collections.Generic;

namespace Data.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Account = new HashSet<Account>();
            Orders = new HashSet<Orders>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Orders> Orders { get; set; }
    }
}
