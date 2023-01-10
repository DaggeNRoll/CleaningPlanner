using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace ResourceServer.Controllers
{
    [Route("api/room")]
    [ApiController]
    public class RoomApiController : ControllerBase
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        public RoomApiController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        public IActionResult GetAllRooms()
        {
            List<RoomViewModel> rooms = _serviceManager.RoomService.GetAllRooms();
            List<RoomApiModel> roomsApi = rooms.Select(r => _serviceManager.RoomService.GetApiModel(r)).ToList();
            return Ok(roomsApi);
        }

        [HttpGet]
        [Route("id/{roomId}")]
        public IActionResult GetRoom(int roomId)
        {
            var room = _serviceManager.RoomService.RoomDbToViewModel(roomId);
            if (room == null)
            {
                return NotFound();
            }

            var roomApi = _serviceManager.RoomService.GetApiModel(room);
            return Ok(roomApi);
        }

        [HttpGet]
        [Route("roomEditor")]
        public IActionResult GetRoomEditModel(int roomId, int userId)
        {
            RoomEditModel editModel = (roomId != 0)
                ? _serviceManager.RoomService.GetRoomEditModel(roomId)
                : _serviceManager.RoomService.CreateRoomEditModel();

            return Ok(editModel);
        }

        [HttpPost("save")]
        public IActionResult SaveRoom(RoomApiModel apiModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Неправильно заполненная форма");
            }

            return Ok(_serviceManager.RoomService.SaveApiModelToDb(apiModel));
        }

        [HttpDelete("delete/id/{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            int result = _serviceManager.RoomService.DeleteRoom(roomId);

            return result switch
            {
                0 => Ok(),
                _ => BadRequest("Не удалось удалить"),
            };
        }
    }
}
