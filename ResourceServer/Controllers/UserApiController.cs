using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

using System.Linq;

namespace ResourceServer.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;
        public UserApiController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var apiModels = _serviceManager.UserService.GetAllUserApiModels();

            return Ok(apiModels);
        }

        [HttpGet]
        [Route("{userID}")]
        public IActionResult GetUser(int userId)
        {
            var apiModel = _serviceManager.UserService.GetApiModelFormDb(userId);
            return Ok(apiModel);
        }

        [HttpPost]
        public IActionResult SaveUser([FromForm] UserApiModel userApiModel)
        {
            var apiModelFromDb = _serviceManager.UserService.SaveApiModelToDb(userApiModel);
            return Ok(apiModelFromDb);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult CreateUser(RegisterViewModel registerViewModel)
        {
            var apiModelFromDb = _serviceManager.UserService.CreateUser(registerViewModel);
            return Ok(apiModelFromDb);
        }

        [HttpDelete]
        [Route("{userId}")]
        public IActionResult DeleteUser(int userId) 
        {
            int serviceResponce = _serviceManager.UserService.DeleteUser(userId);

            return serviceResponce switch
            {
                1 => Ok(),
                _ => BadRequest("Не удалось удалить")
            };

        }
    }
}
