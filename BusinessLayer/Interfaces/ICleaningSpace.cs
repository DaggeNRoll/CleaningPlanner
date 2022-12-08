using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICleaningSpace
    {
        public IEnumerable<CleaningSpace> GetAllCleaningSpaces();
        public IEnumerable<CleaningSpace> GetCleaningSpacesByRoom(Room room);
        public IEnumerable<CleaningSpace> GetCleaningSpacesByUser(User user);
        public CleaningSpace AddCleaningSpace(CleaningSpace space);
        public CleaningSpace UpdateCleaningSpace(CleaningSpace space);
        public int DeleteCleaningSpace(CleaningSpace space);
    }
}
