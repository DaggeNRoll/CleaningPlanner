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
        private string _url;
        private HttpClient _httpClient;

        
        public UserController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
            _httpClient = new HttpClient();
            _url = "https://localhost:44372/api/user";
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var content = await _httpClient.GetStringAsync(_url+$"/id/{userId}");
            var apiModel = JsonConvert.DeserializeObject<UserApiModel>(content);
            var viewModel = _serviceManager.UserService.UserApiModelToView(apiModel);
            viewModel.CleaningSpaces = apiModel.CleaningSpaceIds
                .Select(s => _serviceManager.CleaningSpaceService
                .CleaningSpaceDbToViewModel(s))
                .ToList();
            
            return View(viewModel);
        }

        
        [HttpGet]
        public async Task<IActionResult> Index(string nickname)
        {
            
            //var url = $"https://localhost:44372/api/user/nickname/?nickname={nickname}";
            var content = await _httpClient.GetStringAsync(_url+$"/nickname/?nickname={nickname}");
            var apiModel = JsonConvert.DeserializeObject<UserApiModel>(content);
            var viewModel = _serviceManager.UserService.UserApiModelToView(apiModel);
            viewModel.CleaningSpaces = apiModel.CleaningSpaceIds
                .Select(s => _serviceManager.CleaningSpaceService
                .CleaningSpaceDbToViewModel(s))
                .ToList();
            return View(viewModel);

        }

        /*[Route("delete/{roomId}/{userId}")]
        [HttpGet]
        public IActionResult DeleteRoom(int roomId, int userId)
        {
            return RedirectToAction("DeleteRoome", "Room", new {roomId=roomId, userId=userId });
        }*/ 
    }
}
