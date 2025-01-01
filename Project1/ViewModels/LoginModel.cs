using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RemmberMe { get; set; }
    }

}
