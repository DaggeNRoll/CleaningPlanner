using DataLayer.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface ICleaningSpace
    {
        public IEnumerable<CleaningSpace> GetAllCleaningSpaces();
        public CleaningSpace GetCleaningSpace(CleaningSpace space);
        public CleaningSpace GetCleaningSpaceById(int id);
        public IEnumerable<CleaningSpace> GetCleaningSpacesByRoom(Room room);
        public IEnumerable<CleaningSpace> GetCleaningSpacesByUser(User user);
        public IEnumerable<CleaningSpace> GetCleaningSpacesByUser(int userId);
        public CleaningSpace AddCleaningSpace(CleaningSpace space, Room room);
        public CleaningSpace UpdateCleaningSpace(CleaningSpace space);
        public int DeleteCleaningSpace(CleaningSpace space);
        public void AddUser(CleaningSpace space, User user);
        public void SaveCleaningSpace(CleaningSpace space);
    }
}
