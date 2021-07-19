namespace EventDrivenArchitectureExample.Data.Messages
{
    public class StockCompensationMessage
    {
        public int OrderId { get; set; }
        public string Reason { get; set; }
    }
}
