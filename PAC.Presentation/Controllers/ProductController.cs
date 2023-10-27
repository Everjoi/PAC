using Microsoft.AspNetCore.Mvc;
using PAC.Application.Interfaces.Repository;

namespace PAC.Presentation.Controllers
{
    public class ProductController:Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

         
    }
}
