using EventDrivenArchitectureExample.Data.Context;
using EventDrivenArchitectureExample.Data.Messages;
using EventDrivenArchitectureExample.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Stock.Handler
{
    public class StockHandler : IStockHandler
    {
        protected readonly DataContext _dataContext;

        public StockHandler(DataContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task Handle(OrderCreatedMessage orderCreatedMessage)
        {
            var eventService = new EventMessageService();
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == orderCreatedMessage.ProductId);

            if (product?.StockQuantity >= orderCreatedMessage.Quantity)
            {
                product.StockQuantity -= orderCreatedMessage.Quantity;

                _dataContext.Products.Update(product);
                await _dataContext.SaveChangesAsync();

                var outOfStock = new StockCheckedMessage
                {
                    OrderId = orderCreatedMessage.Id
                };

                await eventService.SendEvent(outOfStock, "stock-checked");
            }
            else
            {
                var outOfStock = new OrderCreatedCompensationMessage
                {
                    OrderId = orderCreatedMessage.Id,
                    Reason = "Produto sem estoque"
                };

                await eventService.SendEvent(outOfStock, "order-created-compensation");
            }
        }

        public async Task Handle(PaymentNotAllowedMessage paymentNotAllowedMessage)
        {
            var eventService = new EventMessageService();
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == paymentNotAllowedMessage.ProductId);

            product.StockQuantity += paymentNotAllowedMessage.Quantity;

            _dataContext.Products.Update(product);
            await _dataContext.SaveChangesAsync();

            var outOfStock = new OrderCreatedCompensationMessage
            {
                OrderId = paymentNotAllowedMessage.OrderId,
                Reason = paymentNotAllowedMessage.Reason
            };

            await eventService.SendEvent(outOfStock, "order-created-compensation");
        }
    }
}
