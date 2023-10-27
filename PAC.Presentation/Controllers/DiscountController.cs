using Microsoft.AspNetCore.Mvc;
using PAC.Application.Interfaces.Repository;
using PAC.Domain.Entitity;

namespace PAC.Presentation.Controllers
{
    public class DiscountController:Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        [HttpDelete] 
        public IActionResult DeleteDiscount(Discount discount)
        {
            _unitOfWork.Repository<Discount>().DeleteAsync(discount);
            _unitOfWork.Save(default);
            return Ok();
        }


        [HttpPost]
        public IActionResult CreateDiscount(Discount discount)
        {
            _unitOfWork.Repository<Discount>().AddAsync(discount);
            _unitOfWork.Save(default);
            return Ok(discount);
        }


        [HttpPut]
        public IActionResult UpdateDiscount(Discount discount)
        {
            _unitOfWork.Repository<Discount>().UpdateAsync(discount);
            _unitOfWork.Save(default);
            return Ok();
        }
        
    }
}
