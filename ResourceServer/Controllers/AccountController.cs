using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using ResourceServer.Models;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult Index()
        {
            return View("SignUp");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new UserIdentity
                {
                    UserDbId = model.UserId,
                    Email = model.Email,
                    UserName = model.NickName,
                };

                var result = await _userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    await _signingManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(UserApiController.CreateUser), nameof(UserApiController), new { registerViewModel = model });

                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
    }
}
