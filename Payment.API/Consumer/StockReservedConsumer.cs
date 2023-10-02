using MassTransit;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Consumer
{
    public class stockReservedEventConsumer : IConsumer<StockReservedEvent>
    {
        private readonly ILogger<stockReservedEventConsumer> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public stockReservedEventConsumer(ILogger<stockReservedEventConsumer> logger, IPublishEndpoint publishEndpoint)
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

                await _publishEndpoint.Publish(new PaymentSuccessedEvent { BuyerID = context.Message.BuyerID, OrderID = context.Message.OrderID });

            }
            else
            {
                _logger.LogInformation($"{context.Message.Payment.TotalPrice} TL was not withdrawn from cradit cart for user ID={context.Message.BuyerID}");

                await _publishEndpoint.Publish(new PaymantFailedEvent { BuyerID = context.Message.BuyerID, OrderID = context.Message.OrderID,Message="not enough balance" });
            }
        }
    }
}
