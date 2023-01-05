using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PresentationLayer;
using PresentationLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            

            var content = await _httpClient.GetStringAsync(_url + $"/roomEditor?roomId={roomId}");
            RoomEditModel roomEditModel=JsonConvert.DeserializeObject<RoomEditModel>(content);

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
        public IActionResult SaveRoom(RoomEditModel editModel)
        {
            var viewModel =_serviceManager.RoomService.SaveRoomEditModelToDb(editModel);
            return RedirectToAction("RoomByRoomId", new { roomId = viewModel.Room.Id });
        }
    }
}
