using ContactManagerApp.Api.Repositories;
using ContactManagerApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ContactManagerApp.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        public string GetNewJwt(User user)
        {
            var key = _configuration.GetValue<string>("JwtKey");
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("username", user.Username),
                new Claim("userId", user.ID.ToString()),
                new Claim("roleid", user.RoleID.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "https://contactmanagerapp.azurewebsites.net",
                audience: "https://contactmanagerapp.azurewebsites.net",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateJwt(string token)
        {
            var jwtToken = GetDecryptedJwt(token);

            var userId = GetUserIdFromDecryptedJwt(jwtToken);

            return _userRepository.GetUserById(userId) != null;
        }

        public string GetJwtFromCookies(HttpContext httpContext)
        {
            return httpContext.Request.Cookies["jwt"];
        }

        public string GetUserRoleIdFromJwt(string token)
        {
            var jwtToken = GetDecryptedJwt(token);

            return GetRoleIdFromDecryptedJwt(jwtToken);
        }

        private JwtSecurityToken GetDecryptedJwt(string token)
        {
            var symmetricKey = GetSymmetricSecurityKey();

            var tokenHandler = new JwtSecurityTokenHandler();

            tokenHandler.ValidateToken(token, GetTokenValidatorParameters(symmetricKey), out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken;
        }

        private SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var key = _configuration.GetValue<string>("JwtKey");
            var encodedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            return encodedKey;
        }

        private static TokenValidationParameters GetTokenValidatorParameters(SecurityKey securityKey)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "https://contactmanagerapp.azurewebsites.net",
                ValidAudience = "https://contactmanagerapp.azurewebsites.net",
                IssuerSigningKey = securityKey,
            };
        }

        private static decimal GetUserIdFromDecryptedJwt(JwtSecurityToken jwtToken)
        {
            return decimal.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);
        }

        private string GetRoleIdFromDecryptedJwt(JwtSecurityToken jwtToken)
        {
            return jwtToken.Claims.First(x => x.Type == "roleid").Value;
        }
    }
}
