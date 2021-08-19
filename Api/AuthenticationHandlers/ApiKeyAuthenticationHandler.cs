using Api.IServices;
using Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Api.AuthenticationHandlers
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string key = "secret-key";

        private readonly IAuthorizationService authorizationService;

        public ApiKeyAuthenticationHandler(
            IAuthorizationService authorizationService,
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
            this.authorizationService = authorizationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(key) && !Request.Query.ContainsKey(key))
            {
                return AuthenticateResult.Fail($"Missing {key} header or query param");
            }

            string apiKey = null;

            if (Request.Headers.ContainsKey(key))
            {
                apiKey = Request.Headers[key];
            }

            if (Request.Query.ContainsKey(key))
            {
                apiKey = Request.Query[key];
            }


            if (!authorizationService.TryAuthenticate(apiKey, out Application application))
            {
                return AuthenticateResult.Fail("Invalid API Key");
            }

            ClaimsIdentity identity = new ClaimsIdentity(Scheme.Name);
            identity.AddClaim(new Claim(ClaimTypes.Name, application.Name));

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);


        }
    }
}
