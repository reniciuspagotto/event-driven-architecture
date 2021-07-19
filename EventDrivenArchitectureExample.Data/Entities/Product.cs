namespace EventDrivenArchitectureExample.Data.Entities
{
    public class Product
    {
        public Product(int id, string name, int stock)
        {
            Id = id;
            Name = name;
            StockQuantity = stock;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
    }
}