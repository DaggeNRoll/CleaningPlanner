using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer;
using PresentationLayer.Models;
using System.Linq;
using System.Net.Http;
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

        
        [HttpGet]
        public async Task<IActionResult> Index(string nickname)
        {
            var client = new HttpClient();
            var url = $"https://localhost:44372/api/user/nickname/?nickname={nickname}";
            var content = await client.GetStringAsync(url);
            var apiModel = JsonConvert.DeserializeObject<UserApiModel>(content);
            var viewModel = _serviceManager.UserService.UserApiModelToView(apiModel);
            viewModel.CleaningSpaces = apiModel.CleaningSpaceIds
                .Select(s => _serviceManager.CleaningSpaceService
                .CleaningSpaceDbToViewModel(s))
                .ToList();
            return View(viewModel);

        }

        
        /*[HttpGet]
        public async Task<IActionResult> IndexByEmail(string email)
        {
            var client = new HttpClient();
            var url = $"https://localhost:44372/api/user/byEmail/{email}";
            var content=await client.GetStringAsync(url);
            var apiModel=JsonConvert.DeserializeObject<UserApiModel>(content);
            var viewModel = _serviceManager.UserService.UserApiModelToView(apiModel);
            viewModel.CleaningSpaces = apiModel.CleaningSpaceIds
                .Select(s=>_serviceManager.CleaningSpaceService
                .CleaningSpaceDbToViewModel(s)).ToList();
            return View("Index",viewModel);

        }*/
    }
}
