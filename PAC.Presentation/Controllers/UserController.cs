using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PAC.Application.Interfaces.Repository;
using PAC.Domain.Entitity;

namespace PAC.Presentation.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpGet]
        public IActionResult GetAllUser()
        {
            var users = _unitOfWork.Repository<User>().GetAllAsync();
            return Ok(users);
        }


        [HttpPut]
        public IActionResult EditUser(User user)
        {
            var result = _unitOfWork.Repository<User>().UpdateAsync(user);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpDelete] 
        public IActionResult DeleteUser(User user)
        {
            _unitOfWork.Repository<User>().DeleteAsync(user);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpGet]
        public IActionResult GetUserById(Guid id)
        {
            var user = _unitOfWork.Repository<User>().GetByIdAsync(id);
            return Ok(user);
        }


        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            var _user = _unitOfWork.Repository<User>().AddAsync(user);
            _unitOfWork.Save(default);
            return Ok(_user);
        }


        [HttpPut]
        public IActionResult AddDiscount(User user,Discount discount)
        {
            user.Discounts.Add(discount);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpPut]
        public IActionResult AddOrder(User user,Order order)
        {
            user.Orders.Add(order);
            _unitOfWork.Save(default);
            return Ok();
        }


    }
}
