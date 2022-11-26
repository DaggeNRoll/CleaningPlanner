using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AuthServer.Models;
using Com;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOptions> authOptions;
        public AuthController(IOptions<AuthOptions> authOptions)
        {
            this.authOptions = authOptions;
        }
        private List<Account> Accounts => new List<Account>//временный локальный репозиторий //TODO сделать бд
        {
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "admin@mail.ru",
                Password = "admin",
                Roles = new Role[] { Role.Admin }
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "master@mail.ru",
                Password = "master",
                Roles = new Role[] { Role.RoomMaster }
            },
            new Account()
            {
                Id = Guid.NewGuid(),
                Email = "user@mail.ru",
                Password = "user",
                Roles = new Role[] { Role.User }
            },
        };

        [Route("login")]//при переходе на путь логина
        [HttpPost]
        public IActionResult Login([FromBody]Login request)//контракт возврата
        {
            var user = AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {
                
            }

            return Unauthorized();
        }

        private Account AuthenticateUser(string email, string password)
        {
            return Accounts.SingleOrDefault(u => u.Email == email && u.Password == password);
        }
        private string GenerateJWT(Account user)
        {
            var authParams = authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim("role", role.ToString()));
            }

            var token = new JwtSecurityToken(authParams.Issuer, authParams.Audience, claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    
    
}