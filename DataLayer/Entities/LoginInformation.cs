namespace DataLayer.Entities
{
    public class LoginInformation
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Hash { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
