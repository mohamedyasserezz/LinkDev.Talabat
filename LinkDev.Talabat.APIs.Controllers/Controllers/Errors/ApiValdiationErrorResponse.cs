using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Errors
{
    internal class ApiValdiationErrorResponse : ApiResponse
    {
        public required IEnumerable<string> Errors { get; set; }
        public ApiValdiationErrorResponse(string? message = null) : base(400, message)
        {
            
        }
    }
}
