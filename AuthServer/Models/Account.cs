using System;

namespace AuthServer.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role[] Roles { get; set; }
        
    }

    public enum Role
    {
        Admin,
        RoomMaster,
        User
    }
}