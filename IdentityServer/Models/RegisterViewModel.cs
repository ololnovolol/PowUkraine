using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models
{
    public class RegisterViewModel
    {
        [Required] 
        public string UserName { get; set; }

        [Required()]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Passwords Confirm")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Date your birthday")]
        public DateTime BirthDay { get; set; }

        public string ReturnUrl { get; set; }
    }
}
