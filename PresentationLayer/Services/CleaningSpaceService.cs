﻿using BusinessLayer;
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
        private RoomService _roomService;
        private UserService _userService;
        public CleaningSpaceService(DataManager dataManager)
        {
            _dataManager = dataManager;
            _roomService=new RoomService(dataManager);
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
                Room=_roomService.RoomDbToViewModel(spaceFromDb.RoomId),
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
            };
        }

        public CleaningSpaceViewModel SaveCleaningSpaceEditModelToDb(CleaningSpaceEditModel spaceEditModel)
        {
            CleaningSpace space;

            if (spaceEditModel.Id != 0)
            {
                space = _dataManager.CleaningSpaceRepository.GetCleaningSpaceById(spaceEditModel.Id);
            }
            else
            {
                space = new CleaningSpace()
                {
                    Id = spaceEditModel.Id,
                    Name = spaceEditModel.Name,
                    Description = spaceEditModel.Description,
                    RoomId = spaceEditModel.RoomId,
                };
            }
            _dataManager.CleaningSpaceRepository.SaveCleaningSpace(space);

            return CleaningSpaceDbToViewModel(space.Id);
        }
    }
}