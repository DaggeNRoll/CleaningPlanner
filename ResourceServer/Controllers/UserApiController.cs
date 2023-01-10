using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

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
        [Route("id/{userID}")]
        public IActionResult GetUser(int userId)
        {
            var apiModel = _serviceManager.UserService.GetApiModelFromDb(userId);
            return Ok(apiModel);
        }

        [HttpGet]
        [Route("nickname")]
        public IActionResult GetUserByNickname(string nickname)
        {
            var apiModel = _serviceManager.UserService.GetApiModelFromDbByNickname(nickname);
            return (apiModel != null) ? Ok(apiModel) : NotFound();
        }

        [HttpGet]
        [Route("email")]
        public IActionResult GetUserByEmail(string email)
        {
            var apiModel = _serviceManager.UserService.GetApiModelFromDbByEmail(email);
            return (apiModel != null) ? Ok(apiModel) : NotFound();
        }



        [HttpPost]
        public IActionResult SaveUser(UserApiModel userApiModel)
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
