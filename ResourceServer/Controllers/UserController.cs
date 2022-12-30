using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using System.Threading.Tasks;

namespace ResourceServer.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        private string url;

        
        public UserController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
            url = "https://localhost:44372/api/user";
        }

        [Route("{userId}")]
        [HttpGet]
        public IActionResult Index(int userId)
        {
            UserViewModel viewModel = _serviceManager.UserService.UserDbModelToView(userId);
            return View(viewModel);
        }

        [Route("{nickname}")]
        [HttpGet]
        public async Task<IActionResult> Index(string nickname)
        {
            
        }
    }
}
