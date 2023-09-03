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

        
        public Task Consume(ConsumeContext<StockReservedEvent> context)
        {
            throw new NotImplementedException();
        }
    }
}
