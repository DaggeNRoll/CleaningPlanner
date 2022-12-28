using BusinessLayer;
using Microsoft.AspNetCore.Http;
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
        [Route("{spaceId}")]
        public IActionResult GetCleaningSpace(int spaceId)
        {
            return Ok(_serviceManager.CleaningSpaceService.GetApiModelFromDb(spaceId));
        }

        [HttpPost]
        public IActionResult SaveCleaningSpace([FromForm] CleaningSpaceApiModel apiModel)
        {
            return Ok(_serviceManager.CleaningSpaceService.SaveApiModelToDb(apiModel));
        }

        [HttpDelete]
        [Route("spaceId")]
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
