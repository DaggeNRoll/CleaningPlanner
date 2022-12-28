using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResourceServer.Models;

namespace ResourceServer.Controllers
{
    
    
    public class AccountController : Controller
    {
        private UserManager<UserIdentity> _userManager;
        private SignInManager<UserIdentity> _signingManager;
        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager)
        {
            _userManager = userManager;
            _signingManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
