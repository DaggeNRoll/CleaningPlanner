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
        //private RoomService _roomService;
        private RoleService _roleService;
        /*private CleaningSpaceService _cleaningSpaceService;*/

        public UserService(DataManager dataManager)
        {
            _dataManager = dataManager;
           // _roomService = new RoomService(_dataManager);
            _roleService = new RoleService(_dataManager);
            /*_cleaningSpaceService = new CleaningSpaceService(_dataManager);*/
        }

        public UserViewModel UserDbModelToView(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);


            /*var rooms = _dataManager.RoomRepository.GetRoomsByUser(userId);
            var cleaningSpaces = _dataManager.CleaningSpaceRepository.GetCleaningSpacesByUser(userId);
            var roles=_dataManager.RoleRepository.GetRolesByUser(userId);*/


            /*var cleaningSpaces = new List<CleaningSpaceViewModel>();*/
            var roles = new List<RoleViewModel>();


            /*foreach (var item in user.Rooms)
            {
                rooms.Add(_roomService.RoomDbToViewModel(item.Id));
            }*/

            /*foreach (var cleaningSpace in user.CleaningSpaces)
            {
                U;
            }*/

            foreach (var role in user.Roles)
            {
                roles.Add(_roleService.RoleDbToViewModel(role.Id));
            }

            return new UserViewModel() { User = user, Roles = roles};
        }

        public UserEditModel GetUserEditModel(int userId)
        {
            var modelFromDb = _dataManager.UserRepository.GetUser(userId);

            var editModel = new UserEditModel()
            {
                Id = userId,
                FullName = modelFromDb.FullName,
                NickName = modelFromDb.NickName,
                RoomId=modelFromDb.RoomId,
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
                    RoomId=editModel.RoomId,
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
                    RoomId=userEditModel.RoomId,
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

        public List<UserViewModel> GetAllUsers()
        {
            var users = _dataManager.UserRepository.GetAllUsers();
            var userModels = new List<UserViewModel>();

            foreach(var user in users)
            {
                userModels.Add(UserDbModelToView(user.Id));
            }

            return userModels;
        }

        public List<UserApiModel> GetAllUserApiModels()
        {
            var userIds = _dataManager.UserRepository.GetAllUsers().Select(u=>u.Id);
            var userApiModels = new List<UserApiModel>();

            foreach(var userId in userIds)
            {
                userApiModels.Add(GetApiModelFormDb(userId));
            }

            return userApiModels;

        }

        public UserApiModel GetApiModelFormDb(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);
            var userApi = new UserApiModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                Nickname = user.NickName,
                RoomId = user.RoomId,
                CleaningSpaceIds = user.CleaningSpaces.Select(s => s.Id).ToList(),
                RoleIds = user.Roles.Select(r => r.Id).ToList(),
            };

            return userApi;
        }

        public UserApiModel SaveApiModelToDb(UserApiModel userApiModel)
        {
            User user;

            if (userApiModel.Id != 0)
            {
                user = _dataManager.UserRepository.GetUser(userApiModel.Id);
               
            }
            else
            {
                user = new User();
            }
            EditUserInformation(ref user, userApiModel);
            _dataManager.UserRepository.SaveUser(user);
            return GetApiModelFormDb(user.Id);
        }

        public int DeleteUser(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);
            return _dataManager.UserRepository.DeleteUser(user);
        }

        private void EditUserInformation(ref User user, UserApiModel userApiModel)
        {
            user.FullName = userApiModel.FullName;
            user.NickName = userApiModel.Nickname;
            user.RoomId= userApiModel.RoomId;
            user.CleaningSpaces = userApiModel.CleaningSpaceIds.Select(cs => _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(cs)).ToList();
            user.Roles=userApiModel.RoleIds?.Select(r=>_dataManager.RoleRepository.GetRole(r)).ToList() ?? new List<Role>();
        }
    }
}
