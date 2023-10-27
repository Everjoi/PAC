using PAC.Application.Mapping;
using PAC.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.DTOs
{
    public class ProductDto : IMapFrom<Product>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }
}
