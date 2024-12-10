using System.ComponentModel.DataAnnotations;

namespace Eticaret.Prj.Models
{
    public class LoginViewModel
    {
        [Display(Name = "E-Posta Adresi"), Required(ErrorMessage = "Email Boş Geçilemez!")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Şifre"), Required(ErrorMessage = "Şifre Boş Geçilemez!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }
}
