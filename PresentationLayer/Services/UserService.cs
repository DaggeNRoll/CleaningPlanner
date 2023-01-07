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


            var role = _roleService.RoleDbToViewModelByUser(userId);
          

            

            return new UserViewModel() { User = user, Role = role};
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
                userApiModels.Add(GetApiModelFromDb(userId));
            }

            return userApiModels;

        }

        public UserApiModel GetApiModelFromDb(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);
            var userApi = new UserApiModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                Nickname = user.NickName,
                RoomId = user.RoomId,
                CleaningSpaceIds = user.CleaningSpaces.Select(s => s.Id).ToList(),
                
                Email=user.Email,
            };

            return userApi;
        }

        public UserApiModel GetApiModelFromDbByNickname(string nickname)
        {
            var user = _dataManager.UserRepository.GetUserByNickname(nickname);
            var userApi = new UserApiModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                Nickname = user.NickName,
                RoomId = user.RoomId,
                CleaningSpaceIds = user.CleaningSpaces.Select(s => s.Id).ToList(),
               
                Email = user.Email,
            };

            return userApi;
        }

        public UserApiModel GetApiModelFromDbByEmail(string email)
        {
            var user = _dataManager.UserRepository.GetUserByEmail(email);
            var userApi = new UserApiModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                Nickname = user.NickName,
                RoomId = user.RoomId,
                CleaningSpaceIds = user.CleaningSpaces.Select(s => s.Id).ToList(),
                
                Email = user.Email,
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
            return GetApiModelFromDb(user.Id);
        }

        public int DeleteUser(int userId)
        {
            var user = _dataManager.UserRepository.GetUser(userId);
            return _dataManager.UserRepository.DeleteUser(user);
        }

        public UserApiModel CreateUser(RegisterViewModel registerViewModel)
        {
            var apiModel = RegisterModelToApi(registerViewModel);
               return SaveApiModelToDb(apiModel);
        }

        public UserApiModel RegisterModelToApi(RegisterViewModel registerViewModel)
        {
            UserApiModel apiModel = new UserApiModel()
            {
                Id = registerViewModel.UserId,
                FullName = registerViewModel.FullName,
                Nickname = registerViewModel.NickName,
                CleaningSpaceIds = new List<int>(),
                Email=registerViewModel.Email,
            };
            return apiModel;
        }

        public UserViewModel UserApiModelToView(UserApiModel apiModel)
        {
            UserViewModel viewModel = new UserViewModel()
            {
                User = _dataManager.UserRepository.GetUser(apiModel.Id),
                
            };

            return viewModel; 
        }

        private void EditUserInformation(ref User user, UserApiModel userApiModel)
        {
            user.FullName = userApiModel.FullName;
            user.NickName = userApiModel.Nickname;
            user.RoomId= userApiModel.RoomId;
            user.CleaningSpaces = userApiModel.CleaningSpaceIds.Select(cs => _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(cs)).ToList();
           
            user.Email=userApiModel.Email;
        }
    }
}
