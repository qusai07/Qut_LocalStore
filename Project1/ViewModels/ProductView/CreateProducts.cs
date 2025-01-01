using Project1.Repository.Enum;
using Project1.Validation;
using System.ComponentModel.DataAnnotations;


namespace Project1.ViewModels.ProductView;

public class CreateProducts
{
    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    [Required]
    [PriceValidation]
    public string Price { get; set; }
    [Required]
    public IFormFile formFile { get; set; }
    


}
