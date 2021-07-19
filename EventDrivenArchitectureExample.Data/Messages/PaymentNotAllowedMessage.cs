namespace EventDrivenArchitectureExample.Data.Messages
{
    public class PaymentNotAllowedMessage
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPurchase { get; set; }
        public string Reason { get; set; }
    }
}
