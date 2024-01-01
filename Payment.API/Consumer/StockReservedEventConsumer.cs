using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Consumer
{
    public class StockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<StockReservedEventConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public StockReservedEventConsumer(ILogger<StockReservedEventConsumer> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        
        public async Task Consume(ConsumeContext<StockReservedEvent> context)
        {

            var balance = 3000m;
            if (balance>context.Message.Payment.TotalPrice)
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was withdrawn cradit card for user ID={context.Message.BuyerID}");

                await _publishEndpoint.Publish(new PaymentCompletedEvent { BuyerID = context.Message.BuyerID, OrderID = context.Message.OrderID });

            }
            else
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was not withdrawn from cradit cart for user ID={context.Message.BuyerID}");

                await _publishEndpoint.Publish(new PaymentFailedEvent { BuyerID = context.Message.BuyerID, OrderID = context.Message.OrderID,Message="not enough balance" ,orderItems=context.Message.OrderItems});
            }
        }
    }
}
