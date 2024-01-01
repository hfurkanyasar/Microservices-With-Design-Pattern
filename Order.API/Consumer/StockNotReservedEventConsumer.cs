using MassTransit;
using Microsoft.Extensions.Logging;
using Order.API.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Consumer
{
    public class StockNotReservedEventConsumer : IConsumer<StockNotReservedEvent>
    {
        private readonly AppDbContext _dbContext;
        private readonly ILogger<StockNotReservedEvent> _logger;

        public StockNotReservedEventConsumer(AppDbContext dbContext, ILogger<StockNotReservedEvent> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
        {
            var order = await _dbContext.Orders.FindAsync(context.Message.OrderID);
            if (order != null)
            {
                order.Status = OrderStatus.Fail;
                order.FailMessage = context.Message.Message;
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Order(ID={context.Message.OrderID}) status chenged : {order.Status}");
            }
            else
            {
                _logger.LogError($"Order(ID={context.Message.OrderID}) not found");
            }
        }
    }
}
