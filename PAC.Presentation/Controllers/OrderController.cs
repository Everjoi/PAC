using Microsoft.AspNetCore.Mvc;
using PAC.Application.Interfaces;
using PAC.Application.Interfaces.Repository;
using PAC.Application.Services;
using PAC.Domain.Entitity;

namespace PAC.Presentation.Controllers
{
    public class OrderController:Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ICostAnalyzerService _analyzerService;

        public OrderController(IUnitOfWork unitOfWork, ICostAnalyzerService costAnalizer)
        {
            _unitOfWork = unitOfWork;
            _analyzerService = costAnalizer;
        }


        [HttpPost ]
        public IActionResult AddOrderItem(Order order, OrderItem item)
        {
            order.OrderItems.Add(item);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpPost]
        public IActionResult AddOrder(Order order, User user)
        {
            var newOrder = order;
            newOrder.TotalPrice = _analyzerService.DiscountPriceCalculation(order,user).Result ;
            _unitOfWork.Repository<Order>().AddAsync(newOrder);
            _unitOfWork.Save(default);
            return Ok();
        }



        [HttpDelete]
        public IActionResult DeleteOrder(Order order)
        {
            _unitOfWork.Repository<Order>().DeleteAsync(order);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpGet] 
        public IActionResult GetAllOrders()
        {
            var orders = _unitOfWork.Repository<Order>().Entities.ToList();
            return Ok(orders);
        }


        [HttpGet]
        public IActionResult GetOrdersByUserId(Guid Id)
        {
            var orders = _unitOfWork.Repository<Order>()
                .Entities
                .ToList().Where(x=>x.UserId == Id).Single();

            return Ok(orders);
        }


        [HttpGet]
        public IActionResult GetOrderItemsByOrder(Order order)
        {
            var orderItems = order.OrderItems.ToList();
            return Ok(orderItems);
        }


    }
}
