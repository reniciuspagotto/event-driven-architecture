using EventDrivenArchitectureExample.Data.Context;
using EventDrivenArchitectureExample.Data.Messages;
using EventDrivenArchitectureExample.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Payment.Handler
{
    public class PaymentHandler : IPaymentHandler
    {
        protected readonly DataContext _dataContext;

        public PaymentHandler(DataContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task Process(StockCheckedMessage stockCheckedMessage)
        {
            var eventService = new EventMessageService();
            var order = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == stockCheckedMessage.OrderId);

            if (order.TotalPurchase > 3000)
            {
                var paymentNotAllowed = new PaymentNotAllowedMessage();
                paymentNotAllowed.ProductId = order.ProductId;
                paymentNotAllowed.OrderId = stockCheckedMessage.OrderId;
                paymentNotAllowed.Reason = "Operação negada pela operadora do cartão";

                await eventService.SendEvent(paymentNotAllowed, "stock-compensation");
            }
            else
            {
                var paymentChecked = new PaymentCheckedMessage();
                paymentChecked.OrderId = stockCheckedMessage.OrderId;

                await eventService.SendEvent(paymentChecked, "payment-checked");
            }
        }
    }
}
