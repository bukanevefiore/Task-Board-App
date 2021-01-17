using System.ComponentModel.DataAnnotations;

namespace TrelloClone.ViewModels
{
    public class LoginViewModel
    {
        // kullanıcı giriş sayfamız için view
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me?")]
        public bool RememberMe { get; set; }
    }
}
