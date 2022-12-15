using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class CleaningSpaceService
    {
        private DataManager _dataManager;
        public CleaningSpaceService(DataManager dataManager)
        {
            _dataManager = dataManager;
        }
        public CleaningSpaceViewModel CleaningSpaceDbToViewModel(int id)
        {
            return new CleaningSpaceViewModel();
        }
    }
}
