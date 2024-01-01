using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;
using Stock.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.API.Consumers
{
    public class PaymentFailedEventConsumer : IConsumer<PaymentFailedEvent>
    {

        private readonly AppDbContext _context;
        private readonly ILogger<PaymentFailedEventConsumer> _logger;

        public PaymentFailedEventConsumer(AppDbContext context, ILogger<PaymentFailedEventConsumer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async  Task Consume(ConsumeContext<PaymentFailedEvent> context)
        {
            foreach (var item in context.Message.orderItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(a=>a.ProductID==item.ProductID);
                if (stock!=null)
                {
                    stock.Count += item.Count;
                    await _context.SaveChangesAsync();
                }

            }
            _logger.LogInformation($"Stock was released for Order ID ({context.Message.OrderID})");
        }
    }
}
