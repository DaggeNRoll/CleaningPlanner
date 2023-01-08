using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using ResourceServer.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceServer.Controllers
{
    
    [Route("account")]
    public class AccountController : Controller
    {
        private UserManager<UserIdentity> _userManager;
        private SignInManager<UserIdentity> _signingManager;
        public AccountController(UserManager<UserIdentity> userManager, SignInManager<UserIdentity> signInManager)
        {
            _userManager = userManager;
            _signingManager = signInManager;
        }

        [Route("register")]
        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [Route("register")]
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
                    await CreateUserInDb(model);
                    return RedirectToAction("Index", "User", new {nickname = model.NickName });

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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signingManager.PasswordSignInAsync(model.Nickname, model.Password, model.IsRememberd, false);

                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "User", new {nickname = model.Nickname});
                    }
                }
                else
                {
                    ModelState.AddModelError("","Неправильный логин или пароль");
                }
            }
            return View(model);
        }

        private async Task<string> CreateUserInDb(RegisterViewModel model)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            var data = new System.Net.Http.StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var url = "https://localhost:44372/api/user/register";
            using var client = new HttpClient();
            var response = await client.PostAsync(url,data);

            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
