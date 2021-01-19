using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public class SendBidEvent : IEvent
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SupplierUserName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
