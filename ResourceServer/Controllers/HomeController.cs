using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PresentationLayer;
using PresentationLayer.Models;
using ResourceServer.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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
