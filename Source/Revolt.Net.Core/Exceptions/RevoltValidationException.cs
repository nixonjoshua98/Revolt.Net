using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revolt.Net
{
    public class RevoltValidationException : RevoltException
    {
        public RevoltValidationException(string message) : base(message)
        {
        }
    }
}
