using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<CleaningSpace> CleaningSpaces { get; set; }
        public int RoomAdminId { get; set; }

    }
}
