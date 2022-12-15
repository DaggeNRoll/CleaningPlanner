using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class RoleService
    {
        private DataManager _dataManager;
        public RoleService(DataManager dataManager)
        {
            _dataManager= dataManager;
        }
        public RoleViewModel RoleDbToViewModel(int id)
        {
            return new RoleViewModel();
        }
    }
}
