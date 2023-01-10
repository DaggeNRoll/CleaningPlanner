using DataLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class CleaningSpaceViewModel
    {
        public CleaningSpace CleaningSpace { get; set; }
        /*public RoomViewModel Room { get; set; }*/
        public List<UserViewModel> Users { get; set; }
    }

    public class CleaningSpaceEditModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }
        public int RoomId { get; set; }
        public List<int> UserIds { get; set; }

    }

    public class CleaningSpaceApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoomId { get; set; }
        public List<int> UserIds { get; set; }
    }
}
