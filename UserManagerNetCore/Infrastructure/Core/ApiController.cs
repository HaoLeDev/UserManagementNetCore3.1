using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using UserManagerNetCore.Infrastructure.Filters;
using UserManagerNetCore.Infrastructure.Services;

namespace UserManagerNetCore.Infrastructure.Core
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiController : ControllerBase
    {

    }
}