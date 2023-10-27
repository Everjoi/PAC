using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Application.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(string email,string password);
        Task<Guid> Register(string FirstName,string LastName,string email,string password);
    }
}
