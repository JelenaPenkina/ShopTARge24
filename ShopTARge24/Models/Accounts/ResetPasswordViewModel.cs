using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.Accounts
{
    public class ResetPasswordViewModel
    {

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Token { get; set; }
    }
}
