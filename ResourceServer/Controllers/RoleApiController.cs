using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleApiController : ControllerBase
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;

        public RoleApiController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRole(int id)
        {
            var role = _serviceManager.RoleService.GetApiModelFromDb(id);
            return role switch
            {
                null => NotFound("Запись не найдена"),
                _ => Ok(role)
            };
        }

        [HttpGet]
        [Route("user/{userId}")]
        public IActionResult GetRoleByUser(int userId)
        {
            var role = _serviceManager.RoleService.GetApiModelFromDbByUser(userId);

            return role switch
            {
                null => NotFound("Запись не найдена"),
                _ => Ok(role)
            };
        }

        [HttpPost]
        public IActionResult RoleEditor(RoleApiModel roleApiModel)
        {
            return Ok(_serviceManager.RoleService.SaveApiModelToDb(roleApiModel));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRole(int id)
        {
            int result = _serviceManager.RoleService.DeleteRole(id);

            return result switch
            {
                0 => Ok(),
                -1 => BadRequest("Удаление не удалось")
            };
        }
    }

}
