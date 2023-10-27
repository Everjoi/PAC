using PAC.Application.Interfaces;
using PAC.Domain.Entitity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.Services
{
    public class CostAnalyzerService : ICostAnalyzerService
    {

        public Task<double> DiscountPriceCalculation(Order order, User user)
        {
            double totalOrderPrice = 0;

            foreach(var orderItem in order.OrderItems)
            {
                double productPrice = orderItem.Product.Price; 
                double discountValue = 0;

                
                var discount = user.Discounts?.FirstOrDefault(d => d.Product?.Id == orderItem.ProductId);
                if(discount != null)
                {
                    discountValue = discount.Value;
                }

                double discountedPrice = productPrice * (1 - discountValue / 100);
                totalOrderPrice += discountedPrice * orderItem.Quantity;
            }

            return Task.FromResult(totalOrderPrice);
        }



    }
}
