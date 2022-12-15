using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class RoleViewModel
    {
        public Role Role { get; set; }
        public UserViewModel User { get; set; }
        public RoomViewModel Room { get; set; }
    }

    public class RoleEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}
