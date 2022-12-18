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
    public class UserService
    {
        private DataManager _dataManager;
        private RoomService _roomService;
        private RoleService _roleService;
        private CleaningSpaceService _cleaningSpaceService;

        public UserService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _roomService = new RoomService(_dataManager);
            _roleService = new RoleService(_dataManager);
            _cleaningSpaceService = new CleaningSpaceService(_dataManager);
        }

        public UserViewModel UserDbModelToView(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);


            /*var rooms = _dataManager.RoomRepository.GetRoomsByUser(userId);
            var cleaningSpaces = _dataManager.CleaningSpaceRepository.GetCleaningSpacesByUser(userId);
            var roles=_dataManager.RoleRepository.GetRolesByUser(userId);*/

            var rooms = new List<RoomViewModel>();

            var cleaningSpaces = new List<CleaningSpaceViewModel>();
            var roles = new List<RoleViewModel>();


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

            return new UserViewModel() { User = user, CleaningSpaces = cleaningSpaces, Roles = roles, Rooms = rooms };
        }

        public UserEditModel GetUserEditModel(int userId)
        {
            var modelFromDb = _dataManager.UserRepository.GetUser(userId);

            var editModel = new UserEditModel()
            {
                Id = userId,
                FullName = modelFromDb.FullName,
                NickName = modelFromDb.NickName,
            };

            return editModel;
        }

        public UserViewModel SaveUserEditModelToDb(UserEditModel editModel)
        {
            User user;

            if (editModel.Id != 0)
            {
                user=_dataManager.UserRepository.GetUser(editModel.Id);

            }
            else
            {
                user = new User()
                {
                    FullName = editModel.FullName,
                    NickName = editModel.NickName,
                };
            }
            _dataManager.UserRepository.SaveUser(user);

            return UserDbModelToView(user.Id);
        }

        public UserViewModel AddCleaningSpaceToUser(UserEditModel userEditModel, CleaningSpaceEditModel cleaningSpaceEditModel)//спросить про многие ко многим
        {
            User user;

            if (userEditModel.Id != 0)
            {
                user = _dataManager.UserRepository.GetUser(userEditModel.Id);

            }
            else
            {
                user = new User()
                {
                    FullName = userEditModel.FullName,
                    NickName = userEditModel.NickName,
                };
            }

            CleaningSpace cleaningSpace;

            if (cleaningSpaceEditModel.Id != 0)
            {
                cleaningSpace = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(cleaningSpaceEditModel.Id);
            }
            else
            {
                cleaningSpace = new CleaningSpace()
                {
                    Name = cleaningSpaceEditModel.Name,
                    Description = cleaningSpaceEditModel.Description,
                    RoomId = cleaningSpaceEditModel.RoomId,
                };
            }

            _dataManager.UserRepository.SaveUser(user);
            _dataManager.CleaningSpaceRepository.SaveCleaningSpace(cleaningSpace);

            _dataManager.UserRepository.AddCleaningSpace(user, cleaningSpace);
            _dataManager.CleaningSpaceRepository.AddUser(cleaningSpace, user);

            return UserDbModelToView(user.Id);
        }
    }
}
