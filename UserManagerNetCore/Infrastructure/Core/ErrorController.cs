using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserManagerNetCore.Infrastructure.Core
{
    [ApiController]
    [Route("api/errors")]
    public class ErrorController : Controller
    {
        [HttpGet("{code}")]
        public async Task<IActionResult> Get(int code)
        {
            return await Task.Run(() =>
            {
                return StatusCode(code, new ProblemDetails()
                {
                    Detail = "See the errors property for details.",
                    Instance = HttpContext.Request.Path,
                    Status = code,
                    Title = ((HttpStatusCode)code).ToString(),
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
                });
            });
        }
    }
}
