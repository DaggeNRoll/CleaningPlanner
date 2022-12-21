using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using System.Collections.Generic;

namespace ResourceServer.Controllers
{
    public class RoomController : Controller
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        public RoomController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<RoomViewModel> roomList = _serviceManager.RoomService.GetAllRooms();
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
        public IActionResult RoomByRoomId(int roomId)
        {
            RoomViewModel room = _serviceManager.RoomService.RoomDbToViewModel(roomId);
            return View("Index",room);
        }


        [HttpGet]
        public IActionResult RoomEditor(int roomId=0, int userId=0)
        {
            RoomEditModel editModel = (roomId!=0)? _serviceManager.RoomService.GetRoomEditModel(roomId)
                : _serviceManager.RoomService.CreateRoomEditModel();

			/*if (roomId!=0)
            {
                editModel = _serviceManager.RoomService.GetRoomEditModel(roomId);
                
            }
            else
            {
                editModel = null;
                
            }*/
			return View("RoomEditor", editModel);
		}

        [HttpPost]
        public IActionResult SaveRoom(RoomEditModel editModel)
        {
            var viewModel =_serviceManager.RoomService.SaveRoomEditModelToDb(editModel);
            return RedirectToAction("RoomByRoomId", new { roomId = viewModel.Room.Id });
        }
    }
}
