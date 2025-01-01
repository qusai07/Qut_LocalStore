using Project1.Repository.Enum;

namespace Project1.ViewModels.ProductView
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category CategoryName { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
    }
}
