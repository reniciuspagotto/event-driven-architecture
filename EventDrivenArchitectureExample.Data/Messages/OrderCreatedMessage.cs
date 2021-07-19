namespace EventDrivenArchitectureExample.Data.Messages
{
    public class OrderCreatedMessage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
