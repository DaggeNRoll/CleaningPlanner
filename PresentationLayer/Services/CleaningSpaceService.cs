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
    public class CleaningSpaceService
    {
        private DataManager _dataManager;
        //private RoomService _roomService;
        private UserService _userService;
        public CleaningSpaceService(DataManager dataManager)
        {
            _dataManager = dataManager;
            //_roomService=new RoomService(dataManager);
            _userService = new UserService(dataManager);
        }

        public CleaningSpaceViewModel CleaningSpaceDbToViewModel(int id)
        {
            var spaceFromDb = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(id);

            var users = new List<UserViewModel>();

            foreach(var user in spaceFromDb.Users)
            {
                users.Add(_userService.UserDbModelToView(user.Id));
            }
            return new CleaningSpaceViewModel()
            { 
                CleaningSpace=spaceFromDb,
                //Room=_roomService.RoomDbToViewModel(spaceFromDb.RoomId),
                Users=users,
            };
        }

        public CleaningSpaceEditModel GetCleaningSpaceEditModel(int id)
        {
            var spaceFromDb = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(id);
            

            return new CleaningSpaceEditModel()
            {
                Id = spaceFromDb.Id,
                Name = spaceFromDb.Name,
                Description = spaceFromDb.Description,
                RoomId = spaceFromDb.RoomId,
                UserIds=spaceFromDb.Users.Select(user=>user.Id).ToList(),
            };
        }

        public CleaningSpaceViewModel SaveCleaningSpaceEditModelToDb(CleaningSpaceEditModel spaceEditModel)
        {
            CleaningSpace space;
            

            if (spaceEditModel.Id != 0)
            {
                space = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(spaceEditModel.Id);
                EnterInformation(ref space, spaceEditModel);
            }
            else
            {
                List<User> users = new List<User>();

                if (spaceEditModel.UserIds == null)
                    spaceEditModel.UserIds = new List<int>();
                foreach(var userId in spaceEditModel.UserIds)
                {
                    users.Add(_dataManager.UserRepository.GetUser(userId));
                }
                space = new CleaningSpace()
                {
                    Id = spaceEditModel.Id,
                    Name = spaceEditModel.Name,
                    Description = spaceEditModel.Description,
                    RoomId = spaceEditModel.RoomId,
                    Users = users,
                };
            }
            _dataManager.CleaningSpaceRepository.SaveCleaningSpace(space);

            return CleaningSpaceDbToViewModel(space.Id);
        }

        public CleaningSpaceEditModel CreateCleaningSpaceEditModel()
        {
            return new CleaningSpaceEditModel();
        }

        public CleaningSpaceApiModel GetApiModelFromDb(int spaceId)
        {
            var spaceFromDb = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(spaceId);
            var spaceApi = new CleaningSpaceApiModel()
            {
                Id = spaceId,
                Name = spaceFromDb.Name,
                Description = spaceFromDb.Description,
                RoomId = spaceFromDb.RoomId,
                UserIds = spaceFromDb.Users.Select(u => u.Id).ToList(),
            };
            return spaceApi;
        }

        public List<CleaningSpaceApiModel> GetAllCleaningSpacesApiModels()
        {
            List<int> spacesFromDbIds = _dataManager.CleaningSpaceRepository.GetAllCleaningSpaces().Select(s=>s.Id).ToList();
            var spacesApi = new List<CleaningSpaceApiModel>();

            foreach (var spaceId in spacesFromDbIds)
            {
                spacesApi.Add(GetApiModelFromDb(spaceId));
            }

            return spacesApi;
            
        }

        public CleaningSpaceApiModel SaveApiModelToDb(CleaningSpaceApiModel apiModel)
        {
            CleaningSpace cleaningSpace;

            if (apiModel.Id != 0)
            {
                cleaningSpace = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(apiModel.Id);
            }
            else
            {
                cleaningSpace = new CleaningSpace();
            }
            EnterInformation(ref cleaningSpace, apiModel);
            _dataManager.CleaningSpaceRepository.SaveCleaningSpace(cleaningSpace);
            return GetApiModelFromDb(cleaningSpace.Id);
        }

        public int DeleteCleaningSpace(int spaceId)
        {
            var space = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(spaceId);
            return _dataManager.CleaningSpaceRepository.DeleteCleaningSpace(space);
        }

        public CleaningSpaceViewModel ApiModelToViewModel(CleaningSpaceApiModel apiModel)
        {
            var viewModel = new CleaningSpaceViewModel
            {
                CleaningSpace = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(apiModel.Id),
                Users = new List<UserViewModel>()
            };
            return viewModel;
        }

        public CleaningSpaceApiModel ViewModelToApiModel(CleaningSpaceViewModel viewModel)
        {
            var apiModel = new CleaningSpaceApiModel
            {
                Id = viewModel.CleaningSpace.Id,
                Name = viewModel.CleaningSpace.Name,
                Description = viewModel.CleaningSpace.Description,
                RoomId = viewModel.CleaningSpace.RoomId,
                UserIds = viewModel.CleaningSpace.Users.Select(u => u.Id).ToList(),
            };
            return apiModel;
        }

        private void EnterInformation(ref CleaningSpace space, CleaningSpaceEditModel editModel)
        {
            List<User> users = space.Users.ToList();
            foreach(var userId in editModel.UserIds)
            {
                users.Add(_dataManager.UserRepository.GetUser(userId));
            }
            space.Name= editModel.Name;
            space.Description= editModel.Description;
            space.RoomId= editModel.RoomId;
            space.Users = users;
        }

        private void EnterInformation (ref CleaningSpace space, CleaningSpaceApiModel apiModel)
        {
            space.Name = apiModel.Name;
            space.Description= apiModel.Description;
            space.RoomId=apiModel.RoomId;
            space.Users=apiModel.UserIds?.Select(sIds=>_dataManager.UserRepository.GetUser(sIds)).ToList() ?? new List<User>();
        }
    }
}
