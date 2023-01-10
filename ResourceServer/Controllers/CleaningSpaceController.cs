using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer;
using PresentationLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResourceServer.Controllers
{
    public class CleaningSpaceController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        private HttpClient _httpClient;
        private string _url;
        public CleaningSpaceController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
            _httpClient = new HttpClient();
            _url = "https://localhost:44372/api/cleaningSpace";
        }

        [HttpGet]
        public async Task<IActionResult> Index(int cleaningSpaceId)
        {
            var content = await _httpClient.GetStringAsync(_url + $"/id?spaceId={cleaningSpaceId}");
            CleaningSpaceApiModel apiModel = JsonConvert.DeserializeObject<CleaningSpaceApiModel>(content);
            CleaningSpaceViewModel viewModel = _serviceManager.CleaningSpaceService.ApiModelToViewModel(apiModel);
            viewModel.Users = apiModel.UserIds
                .Select(uId => _serviceManager.UserService.UserDbModelToView(uId)).ToList();

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> CleaningSpaceEditor(int roomId, int cleaningSpaceId = 0)
        {
            var content = await _httpClient.GetStringAsync(_url + $"/editor?spaceId={cleaningSpaceId}");
            var editModel = JsonConvert.DeserializeObject<CleaningSpaceEditModel>(content);
            if (editModel.Id == 0)
            {
                editModel.RoomId = roomId;
            }

            return View("CleaningSpaceEditor", editModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCleaningSpace(CleaningSpaceEditModel editModel)
        {
            var json = JsonConvert.SerializeObject(editModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url + "/editor", data);
            string result = await response.Content.ReadAsStringAsync();
            var apiModel = JsonConvert.DeserializeObject<CleaningSpaceApiModel>(result);
            var viewModel = _serviceManager.CleaningSpaceService.ApiModelToViewModel(apiModel);
            return RedirectToAction("Index", new { cleaningSpaceId = viewModel.CleaningSpace.Id });
        }


        [HttpGet]
        public async Task<IActionResult> AddPerson(int cleaningSpaceId)
        {
            var content = await _httpClient.GetStringAsync("https://localhost:44372/api/user");
            var userApiModels = JsonConvert.DeserializeObject<List<UserApiModel>>(content);
            List<UserViewModel> userViewModels = userApiModels
                .Select(apiModel => _serviceManager.UserService
                .UserApiModelToView(apiModel)).ToList();

            content = await _httpClient.GetStringAsync(_url + $"/id?spaceId={cleaningSpaceId}");
            CleaningSpaceApiModel spaceApiModel = JsonConvert.DeserializeObject<CleaningSpaceApiModel>(content);
            CleaningSpaceViewModel spaceViewModel = _serviceManager.CleaningSpaceService.ApiModelToViewModel(spaceApiModel);

            PersonCleaningSpaceModel personCleaningSpaceModel = new PersonCleaningSpaceModel
            {
                CLeaningSpace = spaceViewModel,
                Users = userViewModels,
            };
            return View(personCleaningSpaceModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(PersonCleaningSpaceModel formModel)
        {
            PersonCleaningSpaceApiModel formApiModel = new PersonCleaningSpaceApiModel
            {
                CleaningSpace = _serviceManager.CleaningSpaceService.GetApiModelFromDb(formModel.CleaningSpaceId),
                SelectedUserId = formModel.SelectedUserId,
            };

            var json = JsonConvert.SerializeObject(formApiModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url + "/editor/addUser", data);

            var result = await response.Content.ReadAsStringAsync();
            var apiModel = JsonConvert.DeserializeObject<CleaningSpaceApiModel>(result);
            //var viewModel = _serviceManager.CleaningSpaceService.ApiModelToViewModel(apiModel);
            return RedirectToAction("Index", new { cleaningSpaceId = apiModel.Id });
        }
    }
}
