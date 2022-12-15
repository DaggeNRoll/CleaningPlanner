using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Models
{
    public class LoginInformationViewModel
    {
        public LoginInformation LoginInformation { get; set; }
        public UserViewModel User { get; set; }
    }

    public class LoginInformationEditModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }

    }
}
