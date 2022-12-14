using AuthService.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace AuthService.Infrastructure
{
    // dotnet add package System.IdentityModel.Tokens.Jwt
    public class JwtTokenService : ITokenService
    {
        public string Create(User user)
        {
            string secretKey = "your-256-bit-secret";

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var identity = new ClaimsIdentity();

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Username));
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
            identity.AddClaim(new Claim(ClaimTypes.MobilePhone, user.Phone));

            identity.AddClaim(new Claim(ClaimTypes.Role, "Employee"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Developer"));
            identity.AddClaim(new Claim(ClaimTypes.Role, "Trainer"));
            identity.AddClaim(new Claim(ClaimTypes.DateOfBirth, user.Birthday.ToShortDateString()));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://myauthapi.com",
                Audience = "http://myshopper.com",
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = credentials,
                Subject = identity
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);



        }
    }
}
