using Api.IServices;
using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.AuthenticationHandlers
{
    public class ApplicationAuthorizationService : IAuthorizationService
    {
        public bool TryAuthenticate(string apiKey, out Application application)
        {
            application = new Application { Name = "ABC" };

            return apiKey == "your-secret-api-key";
        }
    }
}
