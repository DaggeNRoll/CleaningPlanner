using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace ResourceServer.Controllers
{
    [Route("api/cleaningSpace")]
    [ApiController]
    public class CleaningSpaceApiController : ControllerBase
    {
        private DataManager _dataManager;
        private ServiceManager _serviceManager;

        public CleaningSpaceApiController(DataManager dataManager)
        {
            _dataManager = dataManager;
            _serviceManager = new ServiceManager(dataManager);
        }

        [HttpGet]
        public IActionResult GetAllCleaningSpaces()
        {
            return Ok(_serviceManager.CleaningSpaceService.GetAllCleaningSpacesApiModels());

        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetCleaningSpace(int spaceId)
        {
            var apiModel = _serviceManager.CleaningSpaceService.GetApiModelFromDb(spaceId);
            return apiModel switch
            {
                null => NotFound("Место уборки не найдено"),
                _ => Ok(apiModel),
            };
        }

        [HttpGet]
        [Route("editor")]
        public IActionResult GetCleaningSpaceEditModel(int spaceId)
        {
            return Ok((spaceId != 0) ?
                _serviceManager.CleaningSpaceService.GetCleaningSpaceEditModel(spaceId)
                : new CleaningSpaceEditModel());
        }

        [HttpPost]
        [Route("editor")]
        public IActionResult SaveCleaningSpace(CleaningSpaceApiModel apiModel)
        {

            return Ok(_serviceManager.CleaningSpaceService.SaveApiModelToDb(apiModel));
        }

        [HttpPost]
        [Route("editor/addUser")]
        public IActionResult AddUser(PersonCleaningSpaceApiModel formApiModel)
        {
            CleaningSpaceApiModel apiModel = _serviceManager.CleaningSpaceService.GetApiModelFromDb(formApiModel.CleaningSpace.Id);
            apiModel.UserIds.Add(formApiModel.SelectedUserId);
            _serviceManager.CleaningSpaceService.SaveApiModelToDb(apiModel);
            return Ok(apiModel);
        }

        [HttpDelete]
        [Route("id/{spaceId}")]
        public IActionResult DeleteCleaningSpace(int spaceId)
        {
            int serviceResponce = _serviceManager.CleaningSpaceService.DeleteCleaningSpace(spaceId);

            return serviceResponce switch
            {
                1 => Ok(),
                _ => BadRequest("Удаление не удалось")
            };
        }
    }
}
