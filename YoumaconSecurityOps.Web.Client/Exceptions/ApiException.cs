using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoumaconSecurityOps.Web.Client.Exceptions
{
    public sealed class ApiException : Exception
    {
        public Int32 StatusCode { get; set; }

        public String Content { get; set; }

        public ApiException()
            : base()
        {

        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
