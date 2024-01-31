using System.ComponentModel.DataAnnotations;

namespace Project_Identity_01.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(100,ErrorMessage ="The {0} must be at lest {2} characters long", MinimumLength = 8 )]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Remebers me?")]
        public bool RememberMe { get; set; }
    }
}
