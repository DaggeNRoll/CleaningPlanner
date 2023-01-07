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
            if (room == null)
            {
                return null;
            }
            var users=new List<UserViewModel>();
            var roles = new List<RoleViewModel>();
            var cleaningSpaces = new List<CleaningSpaceViewModel>();

            foreach (var user in room.Users) 
            {
                users.Add(_userService.UserDbModelToView(user.Id));
            }

            if (room.Roles != null) 
            {
				foreach (var role in room.Roles)
				{
					roles.Add(_roleService.RoleDbToViewModel(role.Id));
				}
			}
            

            if (room.CleaningSpaces != null) 
            {
				foreach (var cleaningSpace in room.CleaningSpaces)
				{
					cleaningSpaces.Add(_cleaningSpaceService.CleaningSpaceDbToViewModel(cleaningSpace.Id));
				}
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
                EditRoomInformation(ref room, editModel);
            }
            else
            {
                room = new Room()
                {         
                    Name = editModel.Name,
                    Users=new List<User>() { _dataManager.UserRepository.GetUser(editModel.CreatorId)}
                };
            }
            _dataManager.RoomRepository.SaveRoom(room);

            return RoomDbToViewModel(room.Id);
        }

        public RoomEditModel CreateRoomEditModel()
        {
            return new RoomEditModel();
        }

        public List<RoomViewModel> GetAllRooms()
        {
            var rooms = _dataManager.RoomRepository.GetAllRooms();
            var roomModels=new List<RoomViewModel>();

            foreach(var room in rooms)
            {
                roomModels.Add(RoomDbToViewModel(room.Id));
            }

            return roomModels;
        }

        public Room GetRoomByUser(int userId)
        {

            return _dataManager.RoomRepository.GetRoomByUser(userId);
            
        }

        public RoomApiModel GetApiModel(RoomViewModel viewModel)
        {
            RoomApiModel apiModel = new RoomApiModel()
            {
                Id = viewModel.Room.Id,
                Name = viewModel.Room.Name,
                UserIds = viewModel.Room.Users?.Select(u => u.Id).ToList() ?? new List<int>(),
                RoleIds = viewModel.Room.Roles?.Select(r => r.Id).ToList() ?? new List<int>(),
                CleaningSpaceIds = viewModel.Room.CleaningSpaces.Select(c => c.Id).ToList(),
            };
            return apiModel;
        }

        public RoomApiModel SaveApiModelToDb(RoomApiModel apiModel)
        {
            Room room;

            if (apiModel.Id != 0)
            {
                room=_dataManager.RoomRepository.GetRoomById(apiModel.Id);
               
            }
            else
            {
                room = new Room();
                
            }
            EditRoomInformation(ref room, apiModel);
            _dataManager.RoomRepository.SaveRoom(room);
            return GetApiModel(RoomDbToViewModel(room.Id));
        }

        public int DeleteRoom(int roomId)
        {
            var room = _dataManager.RoomRepository.GetRoomById(roomId);
            return _dataManager.RoomRepository.DeleteRoom(room);
        }

        public RoomViewModel GetViewModelFromApi(RoomApiModel apiModel)
        {
            return RoomDbToViewModel(apiModel.Id);
        }
        
        private void EditRoomInformation(ref Room room, RoomEditModel editModel)
        {
            room.Name= editModel.Name;
        }

        private void EditRoomInformation(ref Room room, RoomApiModel apiModel)
        {
            room.Name = apiModel.Name;
            room.Users = apiModel.UserIds?.Select(ui => _dataManager.UserRepository.GetUser(ui)).ToList() ?? new List<User>();
            room.Roles=apiModel.RoleIds?.Select(ri=>_dataManager.RoleRepository.GetRole(ri)).ToList() ?? new List<Role>();
            room.CleaningSpaces = apiModel.CleaningSpaceIds?.Select(si => _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(si)).ToList() ?? new List<CleaningSpace>();
        }
    }
}
