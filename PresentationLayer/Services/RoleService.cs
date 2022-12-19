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
    }
}
