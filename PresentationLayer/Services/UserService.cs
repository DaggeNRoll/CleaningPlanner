using BusinessLayer;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Services
{
    public class UserService
    {
        private DataManager _dataManager;
        private RoomService _roomService;
        private RoleService _roleService;
        private CleaningSpaceService _cleaningSpaceService;

        public UserService(DataManager dataManager)
        {
            _dataManager= dataManager;
            _roomService = new RoomService(_dataManager);
            _roleService= new RoleService(_dataManager);
            _cleaningSpaceService= new CleaningSpaceService(_dataManager);
        }

        public UserViewModel UserDbModelToView(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);      
           

            /*var rooms = _dataManager.RoomRepository.GetRoomsByUser(userId);
            var cleaningSpaces = _dataManager.CleaningSpaceRepository.GetCleaningSpacesByUser(userId);
            var roles=_dataManager.RoleRepository.GetRolesByUser(userId);*/

            var rooms = new List<RoomViewModel>();

            var cleaningSpaces=new List<CleaningSpaceViewModel>();
            var roles=new List<RoleViewModel>();


            foreach (var item in user.Rooms)
            {
                rooms.Add(_roomService.RoomDbToViewModel(item.Id));
            }

            foreach (var cleaningSpace in user.CleaningSpaces) 
            {
                cleaningSpaces.Add(_cleaningSpaceService.CleaningSpaceDbToViewModel(cleaningSpace.Id));
            }

            foreach (var role in user.Roles)
            {
                roles.Add(_roleService.RoleDbToViewModel(role.Id));
            }

            return new UserViewModel() {User=user, CleaningSpaces=cleaningSpaces,Roles=roles,Rooms=rooms };
        }
    }
}
