using System.ComponentModel.DataAnnotations;

namespace ShopTARge24.Models.Accounts
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword {  get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        // kontrollib, et oleks võrdne uue parooliga 
        [Compare("NewPassword", ErrorMessage = "The new password and confirmed password do not match")]

        public string ConfirmPassword { get; set;} = string.Empty;

    }
}
