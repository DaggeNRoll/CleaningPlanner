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
    public class RoleService
    {
        private DataManager _dataManager;
        //private UserService _userService;
        //private RoomService _roomService;
        public RoleService(DataManager dataManager)
        {
            _dataManager= dataManager;
            /*_userService = new UserService(dataManager);
            _roomService= new RoomService(dataManager);*/
        }
        public RoleViewModel RoleDbToViewModel(int id)
        {
            var role = _dataManager.RoleRepository.GetRole(id);

            return new RoleViewModel()
            { 
                Role=role,
                //User=_userService.UserDbModelToView(role.UserId),
                //Room=_roomService.RoomDbToViewModel(role.RoomId),
            };
        }

        public RoleViewModel RoleDbToViewModelByUser(int userId)
        {
            var role = _dataManager.RoleRepository.GetRoleByUser(userId);

            return new RoleViewModel()
            {
                Role = role,
            };
        }

        public RoleEditModel GetRoleEditModel(int roleId)
        {
            var roleFromDb = _dataManager.RoleRepository.GetRole(roleId);

            return new RoleEditModel()
            {
                Id=roleFromDb.Id,
                Name=roleFromDb.Name,
                Description=roleFromDb.Description,
                UserId=roleFromDb.UserId,
                RoomId=roleFromDb.RoomId,
            };
        }

        public RoleViewModel SaveRoleEditModelToDb(RoleEditModel roleEditModel)
        {
            Role role;

            if (roleEditModel.Id != 0)
            {
                role = _dataManager.RoleRepository.GetRole(roleEditModel.Id);
            }
            else
            {
                role = new Role()
                {
                    Id = roleEditModel.Id,
                    Name = roleEditModel.Name,
                    Description = roleEditModel.Description,
                    UserId = roleEditModel.UserId,
                    RoomId = roleEditModel.RoomId,
                };
            }
            _dataManager.RoleRepository.SaveRole(role);

            return RoleDbToViewModel(role.Id);
        }

        public RoleApiModel GetApiModelFromDb(int id)
        {
            Role role = _dataManager.RoleRepository.GetRole(id);
            RoleApiModel roleApiModel = new RoleApiModel
            {
                Id=role.Id,
                Name=role.Name,
                UserId=role.UserId,
                RoomId=role.RoomId,
            };

            return roleApiModel;
        }

        public RoleApiModel SaveApiModelToDb(RoleApiModel apiModel)
        {
            Role role;
            if(apiModel.Id != 0)
            {
                role = _dataManager.RoleRepository.GetRole(apiModel.Id);
            }
            else
            {
                role = new Role();
            }
            EnterInformation(ref role, apiModel);
            _dataManager.RoleRepository.SaveRole(role);
            apiModel.Id = role.Id;
            return apiModel;
        }

        public int DeleteRole(int id)
        {
            int result = _dataManager.RoleRepository.DeleteRole(id);

            return result;
        }

        private void EnterInformation(ref Role role, RoleApiModel roleApiModel)
        {
            role.Name = roleApiModel.Name;
            role.UserId = roleApiModel.UserId;
            role.RoomId = roleApiModel.RoomId;
        }
    }
}
