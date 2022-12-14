using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class EFCleaningSpaceRepository : ICleaningSpace
    {
        private DSDbContext _context;

        public EFCleaningSpaceRepository(DSDbContext context)
        {
            _context = context;
        }

        public CleaningSpace AddCleaningSpace(CleaningSpace space, Room room)
        {
            var roomFromDb = _context.Rooms.Include(r => r.CleaningSpaces).FirstOrDefault(r => r.Id == room.Id);

            if (roomFromDb == null)
            {
                return null;
            }

            roomFromDb.CleaningSpaces.Add(space);
            _context.SaveChanges();

            return space;
        }

        public void AddUser(CleaningSpace space, User user)
        {
            if (space.Users.Contains(user))
                return;

            space.Users.Add(user);
            _context.SaveChanges();
        }

        public int DeleteCleaningSpace(CleaningSpace space)
        {
            var spaceToBeDeleted = _context.CleaningSpaces.FirstOrDefault(s => s.Id == space.Id);

            if (spaceToBeDeleted == null)
            {
                return -1;
            }

            _context.Remove(spaceToBeDeleted);
            _context.SaveChanges();

            return 0;
        }

        public IEnumerable<CleaningSpace> GetAllCleaningSpaces()
        {
            return _context.CleaningSpaces.Include(s => s.Room).Include(s => s.Users).ToList();
        }

        public CleaningSpace GetCleaningSpace(CleaningSpace space)
        {
            return GetCleaningSpaceById(space.Id);
        }

        public CleaningSpace GetCleaningSpaceById(int id)
        {
            return _context.CleaningSpaces.Include(s => s.Room).Include(s => s.Users).FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<CleaningSpace> GetCleaningSpacesByRoom(Room room)
        {
            return _context.CleaningSpaces.Where(s => s.Room == room).Include(s => s.Room).Include(s => s.Users);
        }

        public IEnumerable<CleaningSpace> GetCleaningSpacesByUser(User user)
        {
            return _context.CleaningSpaces.Include(s => s.Users).Where(s => s.Users.Contains(user)).Include(s => s.Room);
        }

        public IEnumerable<CleaningSpace> GetCleaningSpacesByUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            return _context.CleaningSpaces.Include(s => s.Users).Where(s => s.Users.Contains(user));
        }

        public void SaveCleaningSpace(CleaningSpace space)
        {
            if (space.Id != 0)
            {
                _context.Entry(space).State = EntityState.Modified;
            }
            else
            {
                _context.Add(space);
            }
            _context.SaveChanges();
        }

        public CleaningSpace UpdateCleaningSpace(CleaningSpace space)
        {
            var spaceToBeUpdated = GetCleaningSpace(space);

            if (spaceToBeUpdated == null)
            {
                return null;
            }

            spaceToBeUpdated = space;
            _context.SaveChanges();

            return spaceToBeUpdated;
        }
    }
}
