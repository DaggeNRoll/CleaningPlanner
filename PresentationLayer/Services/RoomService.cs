using BusinessLayer;
using DataLayer.Entities;
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
        private UserService _userService;
        private RoleService _roleService;
        private CleaningSpaceService _cleaningSpaceService;

        public RoomService(DataManager dataManager)
        {
            _dataManager= dataManager;
            _userService=new UserService(dataManager);
            _roleService=new RoleService(dataManager);
            _cleaningSpaceService=new CleaningSpaceService(dataManager);
        }

        
        public RoomViewModel RoomDbToViewModel(int roomId)
        {
            var room=_dataManager.RoomRepository.GetRoomById(roomId);
            var users=new List<UserViewModel>();
            var roles = new List<RoleViewModel>();
            var cleaningSpaces = new List<CleaningSpaceViewModel>();

            foreach (var user in room.Users) 
            {
                users.Add(_userService.UserDbModelToView(user.Id));
            }

            foreach (var role in room.Roles)
            {
                roles.Add(_roleService.RoleDbToViewModel(role.Id));
            }

            foreach(var cleaningSpace in room.CleaningSpaces)
            {
                cleaningSpaces.Add(_cleaningSpaceService.CleaningSpaceDbToViewModel(cleaningSpace.Id));
            }

            return new RoomViewModel() { Room = room, Users = users, Roles = roles, CleaningSpaces = cleaningSpaces };
        }

        public RoomEditModel GetRoomEditModel(int roomId)
        {
            var room = _dataManager.RoomRepository.GetRoomById(roomId);

            var editModel=new RoomEditModel() 
            {
                Id=room.Id,
                Name=room.Name,
            };

            return editModel;
        }

        public RoomViewModel SaveRoomEditModelToDb(RoomEditModel editModel)
        {
            Room room;

            if (editModel.Id != 0)
            {
                room=_dataManager.RoomRepository.GetRoomById(editModel.Id);
            }
            else
            {
                room = new Room()
                {
                    
                    Name = editModel.Name,
                };
            }
            _dataManager.RoomRepository.SaveRoom(room);

            return RoomDbToViewModel(room.Id);
        }
    }
}
