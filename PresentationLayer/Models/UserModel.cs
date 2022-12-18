﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<RoomViewModel> Rooms { get; set; }
        public List<CleaningSpaceViewModel> CleaningSpaces { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }


    public class UserEditModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }

    }
}