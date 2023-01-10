using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class PersonRoomModel
    {
        public RoomApiModel Room { get; set; }
        public List<UserApiModel> Users { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public int SelectedUserId { get; set; }
    }

   
}
