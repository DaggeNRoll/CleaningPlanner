using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;

namespace ResourceServer.Controllers
{
	public class CleaningSpaceController : Controller
	{
		private DataManager _dataManager;
		private ServiceManager _serviceManager;
		public CleaningSpaceController(DataManager dataManager)
		{
			_dataManager = dataManager;
			_serviceManager = new ServiceManager(dataManager);
		}

		[HttpGet]
		public IActionResult Index(int cleaningSpaceId)
		{
			CleaningSpaceViewModel cleaningSpace = _serviceManager.CleaningSpaceService.CleaningSpaceDbToViewModel(cleaningSpaceId);
			return View("Index", cleaningSpace);
		}

		[HttpGet]
		public IActionResult CLeaningSpaceEditor(int cleaningSpaceId=0)
		{
			CleaningSpaceEditModel editModel = (cleaningSpaceId!=0)? _serviceManager.CleaningSpaceService.GetCleaningSpaceEditModel(cleaningSpaceId) 
				: _serviceManager.CleaningSpaceService.CreateCleaningSpaceEditModel();
			return View("CleaningSpaceEditor", editModel);
		}

		[HttpPost]
		public IActionResult SaveCleaningSpace(CleaningSpaceEditModel editModel)
		{
			var viewModel=_serviceManager.CleaningSpaceService.SaveCleaningSpaceEditModelToDb(editModel);
			return RedirectToAction("Index", new { cleaningSpaceId = editModel.Id });
		}

	}
}
