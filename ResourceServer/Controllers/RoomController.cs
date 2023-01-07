using BusinessLayer;
using DataLayer.Entities;
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
    public class RoomController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        private HttpClient _httpClient;
        private string _url;
        public RoomController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
            _httpClient=new HttpClient();
            _url = "https://localhost:44372/api/room";
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var content = await _httpClient.GetStringAsync(_url);

            List<RoomApiModel> roomApiList = JsonConvert.DeserializeObject<List<RoomApiModel>>(content);
            List<RoomViewModel> roomList = roomApiList
                .Select(rApi => _serviceManager.RoomService.GetViewModelFromApi(rApi)).ToList();
            return View(roomList);
        }

        [HttpGet]
        public IActionResult RoomByUserId(int userId)
        {
            var room=_serviceManager.RoomService.GetRoomByUser(userId);
            
            

            if (room != null) 
            {
                RoomViewModel viewModel = _serviceManager.RoomService.RoomDbToViewModel(room.Id);
				return View("Index",viewModel);
			}
            else
            {
                return RedirectToAction("RoomEditor");
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> RoomByRoomId(int roomId)
        {
            var content = await _httpClient.GetStringAsync(_url+$"/id?roomId={roomId}");
            RoomApiModel apiModel = JsonConvert.DeserializeObject<RoomApiModel>(content);
            RoomViewModel room = _serviceManager.RoomService.GetViewModelFromApi(apiModel);
            return View("Index",room);
        }


        [HttpGet]
        public async Task<IActionResult> RoomEditor(int roomId=0, int userId=0)
        {
            

            var content = await _httpClient.GetStringAsync(_url + $"/roomEditor?roomId={roomId}&userId={userId}");
            RoomEditModel roomEditModel=JsonConvert.DeserializeObject<RoomEditModel>(content);
            roomEditModel.CreatorId = userId;

			/*if (roomId!=0)
            {
                editModel = _serviceManager.RoomService.GetRoomEditModel(roomId);
                
            }
            else
            {
                editModel = null;
                
            }*/
			return View("RoomEditor", roomEditModel);
		}

        [HttpPost]
        public async Task<IActionResult> SaveRoom(RoomEditModel editModel)
        {
            RoomApiModel apiModel = new RoomApiModel
            {
                Id = editModel.Id,
                Name = editModel.Name,
            };
            var json = JsonConvert.SerializeObject(apiModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url + "/save", data);
            string result = await response.Content.ReadAsStringAsync();
            apiModel = JsonConvert.DeserializeObject<RoomApiModel>(result);

            var content = await _httpClient.GetStringAsync($"https://localhost:44372/api/user/id/{editModel.CreatorId}");
            var userApiModel = JsonConvert.DeserializeObject<UserApiModel>(content);
            userApiModel.RoomId=apiModel.Id;
            json = JsonConvert.SerializeObject(userApiModel);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _httpClient.PostAsync("https://localhost:44372/api/user",data);
            result = await response.Content.ReadAsStringAsync();
            userApiModel = JsonConvert.DeserializeObject<UserApiModel>(result);

            RoleApiModel roleApiModel = new RoleApiModel
            {
                Name = RoleType.Admin.ToString(),
                UserId = userApiModel.Id,
                RoomId = apiModel.Id,
            };
            json = JsonConvert.SerializeObject(roleApiModel);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            response = await _httpClient.PostAsync("https://localhost:44372/api/role", data);



            /*if (editModel.Id == 0)
                apiModel.UserIds = new List<int> { editModel.CreatorId };*/

            //var viewModel =_serviceManager.RoomService.SaveRoomEditModelToDb(editModel);
            return RedirectToAction("Index", "User", new { userId = userApiModel.Id });
        }

        [HttpGet]
        [Route("delete/{roomId}/{userId}", Name ="Delete")]
        public async Task<IActionResult> DeleteRoom(int roomId, int userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(_url + $"/delete/id/{roomId}");
                response.EnsureSuccessStatusCode();
                return RedirectToAction("Index", "User", new {userId});
            }
            catch(HttpRequestException)
            {
                return RedirectToAction("Error", "Home");
            }
            
        }
    }
}
