using EventBusRabbitMQ.Events.Interfaces;
using System;

namespace EventBusRabbitMQ.Events
{
    public class SendBidEvent : IEvent
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
