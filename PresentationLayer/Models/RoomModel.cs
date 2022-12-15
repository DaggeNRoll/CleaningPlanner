using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class RoomViewModel
    {
        public Room Room { get; set; }

        public List<UserViewModel> Users { get; set; }
        public List<RoleViewModel> Roles { get; set; }
        public List<CleaningSpaceViewModel> CleaningSpaces { get; set; }
    }

    public class RoomEditModel {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
