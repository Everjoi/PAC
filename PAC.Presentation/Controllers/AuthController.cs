using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PAC.Application.Interfaces;
using PAC.Application.Interfaces.Repository;
using System.Security.Claims;
using PAC.Application.DTOs;
using Microsoft.AspNetCore.Authentication;
using IAuthenticationService = PAC.Application.Interfaces.IAuthenticationService;
using PAC.Domain.Entitity;
using AutoMapper;

namespace PAC.Presentation.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authentication;
        private readonly IMapper _mapper;

        public AuthController(IUnitOfWork unitOfWork,IAuthenticationService authentication)
        {
            _unitOfWork = unitOfWork;
            _authentication = authentication;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserDto model)
        {
            var token = _authentication.Authenticate(model.Email,model.Password);

            if(token == null)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.NameIdentifier, token)
            };

            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity));

            return RedirectToAction("GetAllTests","Test");
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto model)
        {
            var userRegistered = _authentication.Register(model.FirstName, model.LastName,model.Email,model.Password);

            if(userRegistered.Result == Guid.Empty)
                return BadRequest("Username already exists or there was an issue with registration.");

            var token = _authentication.Authenticate(model.Email,model.Password);

            if(token == null)
                return BadRequest("Issue generating token after registration.");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(ClaimTypes.NameIdentifier, token),
                new Claim(ClaimTypes.Actor,userRegistered.ToString())
            };

            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(identity));
            
            _unitOfWork.Repository<User>().AddAsync((User)_mapper.Map(model,typeof(UserDto),typeof(User)));


            return RedirectToAction("GetAllTests","Test");
        }

    }
}
