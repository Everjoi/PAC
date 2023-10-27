using PAC.Domain.Common.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Domain.Entitity
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; } 
        public string Code { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public ICollection<Discount>? Discounts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
