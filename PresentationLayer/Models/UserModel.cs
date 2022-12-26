using DataLayer.Entities;
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
        //public Room Room { get; set; }
        public List<CleaningSpaceViewModel> CleaningSpaces { get; set; }
        public List<RoleViewModel> Roles { get; set; }

    }


    public class UserEditModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public int? RoomId { get; set; }

    }

    public class UserApiModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Nickname { get; set; }
        public int? RoomId { get; set; }

        public List<int> CleaningSpaceIds { get; set; }
        public List<int> RoleIds { get; set; }
    }
}
