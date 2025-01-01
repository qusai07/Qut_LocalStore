using Project1.Repository.Enum;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public User user { get; set; }
        public  int UserId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; } // Path to the image
    //    public string Category { get; set; }

    }
}
