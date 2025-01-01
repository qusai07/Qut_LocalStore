using Project1.Validation;
using System.ComponentModel.DataAnnotations;

namespace Project1.ViewModels
{
    public class BuyProductModel
    {
        [Required]
        [MaxLength(200)]
        public string ProductDescription { get; set; }
        [Required]
        public string NumberBuyer { get; set; }

        [Required]
        public string EmailBuyer { get; set; }
        [Required]
        public string ProductName { get; set; }  
        [Required]
        public decimal ProductPrice { get; set; }
        [Required]
        public string NameUser { get; set; }
      
    }
}
