using EventDrivenArchitectureExample.Data.Context;
using EventDrivenArchitectureExample.Data.Messages;
using EventDrivenArchitectureExample.Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Order.Handlers
{
    public class OrderHandler : IOrderHandler
    {
        protected readonly DataContext _dataContext;

        public OrderHandler(DataContext dbContext)
        {
            _dataContext = dbContext;
        }

        public async Task<Data.Entities.Order> Create(Data.Entities.Order order)
        {
            order.Status = "Pedido em andamento";
            await _dataContext.Orders.AddAsync(order);
            await _dataContext.SaveChangesAsync();

            var messageEvent = new OrderCreatedMessage
            {
                Id = order.Id,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            };

            var eventService = new EventMessageService();

            await eventService.SendEvent(messageEvent, "order-created");

            return order;
        }

        public async Task Cancel(OrderCreatedCompensationMessage outOfStockMessage)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == outOfStockMessage.OrderId);
            order.Status = $"Pedido cancelado - {outOfStockMessage.Reason}";

            _dataContext.Orders.Update(order);
            await _dataContext.SaveChangesAsync();
        }

        public async Task Finalize(PaymentCheckedMessage paymentCheckedMessage)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == paymentCheckedMessage.OrderId);
            order.Status = "Pedido concluído com sucesso";

            _dataContext.Orders.Update(order);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Data.Entities.Order> GetById(int orderId)
        {
            return await _dataContext.Orders.FirstOrDefaultAsync(p => p.Id == orderId);
        }
    }
}
