
namespace Supermarket.API.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //property to hold the products in this category
        public ICollection<Products> Products { get; set; } 
    }
}
