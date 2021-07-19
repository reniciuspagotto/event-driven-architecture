using EventDrivenArchitectureExample.Data.Messages;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Payment.Handler
{
    public interface IPaymentHandler
    {
        Task Process(StockCheckedMessage outOfStockMessage);
    }
}
