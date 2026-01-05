using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.Accounts
{
    public class LoginViewModel
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        [Required]
        public string ClientID { get; set; } = string.Empty;
        [Required]
        public string? ClientSecret { get; set; }
    }
}
