using Microsoft.AspNetCore.Identity;

namespace Project1.Models
{
    public class User :  IdentityUser<int>
    {
        public List<Product> Products { get; set; }
    }
}
