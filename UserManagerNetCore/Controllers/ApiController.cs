using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserManagerNetCore.Infrastructure.Filters;

namespace UserManagerNetCore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {
        protected async Task<ActionResult> CreateResponse(Func<ActionResult> function)
        {
            try
            {
                var response =  function.Invoke();
                return response;
            }
            catch (Exception ex)
            {
                var resultFilter = new ResultFilter<DBNull>(null, StatusCodes.Status400BadRequest, ex.ToString());
                return BadRequest(resultFilter);
            }
        }
    }
}