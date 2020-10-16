using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace User.Commons.Services
{
    public interface ICurrentUserService
    {
        string GetUserName();

        string GetId();
    }
}
