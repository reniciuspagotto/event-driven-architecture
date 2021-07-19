using EventDrivenArchitectureExample.Data.Messages;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Stock.Handler
{
    public interface IStockHandler
    {
        Task Handle(OrderCreatedMessage orderCreatedMessage);
        Task Handle(PaymentNotAllowedMessage paymentNotAllowedMessage);
    }
}
