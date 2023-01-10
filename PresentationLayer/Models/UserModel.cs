using DataLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        //public Room Room { get; set; }
        public List<CleaningSpaceViewModel> CleaningSpaces { get; set; }

        public RoleViewModel Role { get; set; }

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
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Nickname { get; set; }
        [Required]
        public string Email { get; set; }
        public int? RoomId { get; set; }

        public List<int> CleaningSpaceIds { get; set; }

    }
}
