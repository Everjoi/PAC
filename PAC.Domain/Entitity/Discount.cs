using PAC.Domain.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Domain.Entitity
{
    public class Discount : IEntity
    {
        public Guid Id { get; set; }
        public Product? Product { get; set; }
        public double Value { get; set; }
    }
}
