using ContactManagerApp.Models;
using ContactManagerApp.Api.Repositories;
using ContactManagerApp.Api.Requests;
using ContactManagerApp.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ContactManagerApp.Api.Services;
using Microsoft.AspNetCore.Authorization;
using ContactManagerApp.Api.Filters;

namespace ContactManagerApp.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public AccountController(IConfiguration configuration, IUserRepository userRepository, IAuthService authService)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var response = new LoginResponse();
            try
            {
                var user = _userRepository.GetUserByUsernameAndPass(request.Username, request.Password);

                if (user == null)
                {
                    throw new ArgumentNullException("user");
                }
                var jwt = _authService.GetNewJwt(user);

                Response.Cookies.Delete("jwt");
                Response.Cookies.Append("jwt", jwt);

                return Ok(new { jwt });
            }
            catch (ArgumentNullException)
            {
                response.Errors.Add("UnauthorizedError", new List<string> { "Invalid credentials provided." });
                return StatusCode((int)HttpStatusCode.Unauthorized, response);
            }
            catch (Exception ex)
            {
                response.Errors.Add("GeneralError", new List<string> { "Something went wrong. " + ex.Message });
                return StatusCode((int)HttpStatusCode.Unauthorized, response);
            }
        }

        [JwtValidation]
        [HttpGet("authorize")]
        public IActionResult Authorize()
        {
            var jwt = _authService.GetJwtFromCookies(HttpContext);
            if (_authService.GetUserRoleIdFromJwt(jwt) == "1")
                return Ok();
            else
                return Unauthorized();
        }
    }

}
