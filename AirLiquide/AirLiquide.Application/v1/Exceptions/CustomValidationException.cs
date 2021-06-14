using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirLiquide.Application.v1.Exceptions
{
    public class CustomValidationException : ApplicationException
    {
        public CustomValidationException(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public CustomValidationException(IEnumerable<string> errors, string message, Exception innerException) : base(message, innerException)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; private set;  }
    }
}
