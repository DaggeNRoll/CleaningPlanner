using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRoom
    {
        public IEnumerable<Room> GetAllRooms();
        //public IEnumerable<Room> GetRoomsByUser(int userID);
        //public IEnumerable<Room> GetRoomsByUser(User user);
        public Room GetRoomById(int roomID);
        public Room AddRoom(Room room);
        /*public Room AddRoom(Room room, User user);*/
        public Room UpdateRoom(Room room);
        public int DeleteRoom(Room room);

        public void SaveRoom(Room room);
    }
}
