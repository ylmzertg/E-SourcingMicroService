using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ.Events
{
    public abstract class IEvent
    {
        public Guid RequestId { get; set; }

        public IEvent()
        {
            RequestId = Guid.NewGuid();
        }
    }
}
