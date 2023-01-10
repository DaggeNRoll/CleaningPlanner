using BusinessLayer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Implementations
{
    public class EFRoomRepository : IRoom
    {
        private DSDbContext _context;

        public EFRoomRepository(DSDbContext context)
        {
            _context = context;
        }

        public Room AddRoom(Room room)
        {
            _context.Add(room);
            _context.SaveChanges();
            return room;
        }

        /*public Room AddRoom(Room room, User user)//проверить на практике, может не работать. а лучше вообще сделать потом;) //TODO
        {
            var userFromDb = _context.Users.Include(r=>r.Rooms).FirstOrDefault(u=>u.Id==user.Id);

            if (user != null)
            {
                userFromDb.Rooms.Add(room);
                _context.SaveChanges();
                return room;
            }
            return null;
        }*/

        public int DeleteRoom(Room room)
        {
            try
            {
                _context.Remove(room);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public Room GetRoomById(int roomID)
        {
            return _context.Rooms.Include(r => r.Roles).Include(r => r.Users).Include(r => r.CleaningSpaces).FirstOrDefault(r => r.Id == roomID);
        }

        /*public IEnumerable<Room> GetRoomsByUser(int userID)
        {
            var user = _context.Users.FirstOrDefault(user=>user.Id==userID);
            if (user == null)
            {
                return Enumerable.Empty<Room>();
            }

            return user.Rooms;
        }

        public IEnumerable<Room> GetRoomsByUser(User user)
        {
            var userFromDb = _context.Users.FirstOrDefault(u => u == user);
            if (userFromDb == null)
            {
                return Enumerable.Empty<Room>();
            }

            return userFromDb.Rooms;
        }*/

        public void SaveRoom(Room room)
        {
            if (room.Id != 0)
            {
                _context.Entry(room).State = EntityState.Modified;
            }
            else
            {
                _context.Add(room);
            }

            _context.SaveChanges();
        }

        public Room UpdateRoom(Room room)
        {
            var roomToBeUpdated = _context.Rooms.FirstOrDefault(r => r.Id == room.Id);

            if (roomToBeUpdated == null)
            {
                return null;
            }

            roomToBeUpdated = room;
            _context.SaveChanges();
            return roomToBeUpdated;
        }
        public Room GetRoomByUser(int userId)
        {
            var user = _context.Users.Include(u => u.Room).FirstOrDefault(u => u.Id == userId);
            return user.Room;
        }
    }
}
