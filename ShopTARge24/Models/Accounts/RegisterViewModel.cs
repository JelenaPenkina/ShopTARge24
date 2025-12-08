using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ShopTARge24.Models.Accounts
{
    public class RegisterViewModel
    {
        [Required]
       // [ValidEmailDomain()]
        [EmailAddress]       
        public string Email { get; set; }
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string City { get; set; }

    }
}
