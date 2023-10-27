using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Domain.Exceptions
{
    public class WrongPasswordException : Exception
    {
        public WrongPasswordException() : base("Wrong password!")
        {
            
        }
    }
}
