using PAC.Application.Mapping;
using PAC.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.DTOs
{
    public class DiscountDto : IMapFrom<Discount>
    {
        public Guid Id { get; set; }
        public Product? Product { get; set; }
        [Range(0,1,ErrorMessage = "Value must be between 0 and 1.")]
        public double Value { get; set; }
    }
}
