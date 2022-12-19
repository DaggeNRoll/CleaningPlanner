using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace ResourceServer.Controllers
{
    public class UserController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;

        public UserController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        public IActionResult Index(int userId)
        {
            UserViewModel viewModel = _serviceManager.UserService.UserDbModelToView(userId);
            return View(viewModel);
        }
    }
}
