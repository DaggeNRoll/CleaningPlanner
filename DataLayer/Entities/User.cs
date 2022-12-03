using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public LoginInformation LoginInformation { get; set; }
        public ICollection<CleaningSpace> CleaningSpaces { get; set; }
        public ICollection<Role> Roles { get; set; }

    }
}
