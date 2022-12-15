using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class RoomService
    {
        private DataManager _dataManager;
        public RoomService(DataManager dataManager)
        {
            _dataManager= dataManager;
        }

        
        public RoomViewModel RoomDbToViewModel(int roomId)
        {
            return new RoomViewModel();
        }
    }
}
