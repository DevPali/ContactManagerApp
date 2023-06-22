using ContactManagerApp.Models;

namespace ContactManagerApp.Api.Services
{
    public interface IAuthService
    {
        string GetNewJwt(User user);
        bool ValidateJwt(string token);
        string GetJwtFromCookies(HttpContext httpcontext);
        string GetUserRoleIdFromJwt(string token);
    }
}
