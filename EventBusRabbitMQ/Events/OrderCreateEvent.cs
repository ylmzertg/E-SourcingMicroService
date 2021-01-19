using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public class OrderCreateEvent : IEvent
    {
        public string AuctionId { get; set; }
    }
}
