namespace EventDrivenArchitectureExample.Data.Messages
{
    public class OrderCreatedCompensationMessage
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}
