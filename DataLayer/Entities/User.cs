using System.Collections.Generic;

namespace DataLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }
        public LoginInformation LoginInformation { get; set; }
        public ICollection<CleaningSpace> CleaningSpaces { get; set; }
        public Role Role { get; set; }

    }
}
