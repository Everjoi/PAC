using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAC.Domain.Exceptions
{
    public class NotFoundException  :Exception
    {
        public NotFoundException(Type type) : base($"Type: {type} was not found ")
        {
                
        }
    }
}
