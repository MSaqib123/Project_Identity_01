using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Project_Identity_01.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remebers me?")]
        public bool RememberMe { get; set; }
        //___ Return URL ____
        public string ReturnURL { get; set; }
    }
}
