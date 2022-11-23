using System;
using System.Collections.Generic;
using System.Linq;
using AuthServer.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
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
    }
    
    
}