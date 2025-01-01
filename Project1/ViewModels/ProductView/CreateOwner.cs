using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels.ProductView
{
    public class CreateOwner
    {
        [Required]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and ConfirmPassword in not Match")]
        public string ConfPassword { get; set; }

        [Required]
        public string PhoneNumebr { get; set; }
        [Required]
        public string NormalizedUserName { get; set; }
    }
}
