using ContactManagerApp.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ContactManagerApp.Api.Filters
{
    public class JwtValidationAttribute : TypeFilterAttribute
    {
        public JwtValidationAttribute() : base(typeof(JwtValidationFilter))
        {
        }
    }
    public class JwtValidationFilter : IAuthorizationFilter
    {
        private readonly IAuthService _authService;

        public JwtValidationFilter(IAuthService authService)
        {
            _authService = authService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var jwtToken = _authService.GetJwtFromCookies(context.HttpContext) ?? "";

                if (!_authService.ValidateJwt(jwtToken))
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                context.Result = new RedirectToActionResult("index", "home", null);
            }
        }
    }
}
