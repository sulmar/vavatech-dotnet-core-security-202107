using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.IServices
{

    public interface IAuthorizationService
    {
        bool TryAuthenticate(string apiKey, out Application application);
    }
}
