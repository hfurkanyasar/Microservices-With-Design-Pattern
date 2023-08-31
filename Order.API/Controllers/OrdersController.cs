using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.DTOs;
using Order.API.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;
        public OrdersController(AppDbContext context, IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateDTO orderCreate)
        {

            var newOrder = new Models.Order
            {

                BuyerID = orderCreate.BuyerID,
                Status = OrderStatus.Suspend,
                Adress = new Adress
                {
                    Line = orderCreate.adres.Line,
                    Province = orderCreate.adres.Province,
                    District = orderCreate.adres.District
                },
                CreatedDate = DateTime.Now
            };
            orderCreate.orderItems.ForEach(item =>
            {
                newOrder.orderItems.Add(new OrderItem() { Price = item.Price, ProductID = item.ProductID, Count = item.Count });

            });
            await _context.AddAsync(newOrder);
            await _context.SaveChangesAsync();

            var OrderCreatedEvent = new OrderCreatedEvent()
            {
                BuyerID = orderCreate.BuyerID,
                OrderID = newOrder.OrderID,
                Payment = new PaymentMesagge
                {
                    CardName = orderCreate.payment.CardName,
                    CardNumber = orderCreate.payment.CardNumber,
                    Expiration = orderCreate.payment.Expiration,
                    CVV = orderCreate.payment.CVV,
                    TotalPrice = orderCreate.orderItems.Sum(x => x.Price * x.Count)
                },


            };
            orderCreate.orderItems.ForEach(item =>
            {
                OrderCreatedEvent.orderItems.Add(new OrderItemMessage { Count = item.Count, ProductID = item.ProductID });


            });

            await _publishEndpoint.Publish(OrderCreatedEvent);

            return Ok();
        }
    }
}
