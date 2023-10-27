using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PAC.Application.Interfaces;
using PAC.Application.Interfaces.Repository;
using PAC.Domain.Entitity;
using PAC.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;


        public AuthenticationService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {

            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.Repository<User>();

        }


        public string Authenticate(string email, string password)
        {
            var user = _userRepository.Entities.ToList().Find(x => x.Email == email);

            if (user == null)
                throw new NotFoundException(user.GetType());

            if (!VerifyPassword(password, user.Password))
                throw new WrongPasswordException();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Actor, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        public async Task<Guid> Register(string firstName, string lastName, string email, string password)
        {
            var existingUser = await _unitOfWork.Repository<User>().GetAllAsync();
            if (existingUser.Any(x => x.Email == email))
                return Guid.Empty;

            var hashedPassword = HashPassword(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Code = Guid.NewGuid().ToString(),
                Email = email,
                Password = hashedPassword
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.Save(default);

            return user.Id;
        }


        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }


    }
}
