namespace EventDrivenArchitectureExample.Data.Entities
{
    public class Product
    {
        public Product(int id, string name, int stockQuantity)
        {
            Id = id;
            Name = name;
            StockQuantity = stockQuantity;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
    }
}