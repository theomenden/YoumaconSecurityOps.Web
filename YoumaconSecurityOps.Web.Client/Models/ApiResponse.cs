using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoumaconSecurityOps.Core.Shared.Enumerations;

namespace YoumaconSecurityOps.Web.Client.Models
{
    public class ApiResponse<T>
    {
        /// <value>
        /// The value that our api returns
        /// </value>
        /// <remarks>On Success should return <typeparamref name="T"/>: On Failure should be <c>Null</c></remarks>
        public T Data { get; set; }

        /// <value>
        /// Status code from the API
        /// </value>
        public ResponseCodes ResponseCode { get; set; }

        /// <value>
        /// Human Readable Representation of the Response Code
        /// </value>
        public String ResponseMessage { get; set; }
    }
}
