using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles)
        {
            if (roles != null)
                Roles = string.Join(",", roles);
        }
    }
}
