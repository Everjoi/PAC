using PAC.Application.Mapping;
using PAC.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.DTOs
{
    public class UserDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public ICollection<Discount>? Discounts { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
