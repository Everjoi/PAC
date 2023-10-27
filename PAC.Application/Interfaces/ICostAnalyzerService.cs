using PAC.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.Interfaces
{
    public interface ICostAnalyzerService
    {
        Task<double> DiscountPriceCalculation(Order order,User user);
    }
}
