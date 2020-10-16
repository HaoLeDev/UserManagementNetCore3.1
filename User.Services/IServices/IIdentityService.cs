using System;
using System.Collections.Generic;
using System.Text;

namespace User.Services.Services
{
    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
    }

}
