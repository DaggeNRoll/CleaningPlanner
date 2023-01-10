using DataLayer.Entities;

namespace PresentationLayer.Models
{
    public class LoginInformationViewModel
    {
        public LoginInformation LoginInformation { get; set; }
        /*public UserViewModel User { get; set; }*/
    }

    public class LoginInformationEditModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }

    }
}
