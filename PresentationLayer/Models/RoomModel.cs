using DataLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class RoomViewModel
    {
        public Room Room { get; set; }

        public List<UserViewModel> Users { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<CleaningSpaceViewModel> CleaningSpaces { get; set; }
    }

    public class RoomEditModel
    {

        public int Id { get; set; }
        [Required]
        [Display(Name="Название")]
        public string Name { get; set; }
        public int CreatorId { get; set; }
        public int RoomAdminId { get; set; }
    }

    public class RoomApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomAdminId { get; set; }

        public List<int> UserIds { get; set; }
        public List<int> RoleIds { get; set; }
        public List<int> CleaningSpaceIds { get; set; }
    }
}
