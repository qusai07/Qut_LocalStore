using Project1.Models;

namespace Project1.ViewModels
{
    public class ListOfInfo
    {

        public IEnumerable<Product> products { get; set; }
        public IEnumerable<TheBuyer> theBuyers {  get; set;}

        public List<User> users { get; set;}
    }
}
