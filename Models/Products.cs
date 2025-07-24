namespace Supermarket.API.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int quantityInPackage { get; set; }
        public EUnitOfMeasurement unitOfMeasurement { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
