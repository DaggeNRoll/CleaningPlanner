using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using ResourceServer.Models;
using System.Diagnostics;

namespace ResourceServer.Controllers
{
    public class HomeController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;

        public HomeController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Index", "User", new { nickname = User.Identity.Name });
            }
            return RedirectToAction("Login", "Account");
            /*List<UserViewModel> users = _serviceManager.UserService.GetAllUsers();
            return View(users);*/
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
