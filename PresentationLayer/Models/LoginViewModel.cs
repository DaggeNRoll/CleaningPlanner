using System.ComponentModel.DataAnnotations;

namespace PresentationLayer.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string Nickname { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Запомнить?")]
        public bool IsRememberd { get; set; }

        public string ReturnUrl { get; set; }
    }
}
