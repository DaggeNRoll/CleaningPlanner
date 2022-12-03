using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class CleaningSpace
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Room Room { get; set; }
        public int RoomId { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
