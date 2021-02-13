using AutoMapper;
using EventBusRabbitMQ;
using EventBusRabbitMQ.Core;
using EventBusRabbitMQ.Events;
using MediatR;
using Newtonsoft.Json;
using Ordering.Application.Commands;
using Ordering.Core.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.Order.Consumers
{
    public class EventBusOrderCreateConsumer
    {
        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repository; // we added this in order to resolve in mediatR

        public EventBusOrderCreateConsumer(IRabbitMQPersistentConnection persistentConnection, IMediator mediator, IMapper mapper, IOrderRepository repository)
        {
            _persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Consume()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var channel = _persistentConnection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.OrderCreateQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.OrderCreateQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.OrderCreateQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var orderCreateEvent = JsonConvert.DeserializeObject<OrderCreateEvent>(message);

                // EXECUTION : Call Internal Order Operation
                var command = _mapper.Map<OrderCreateCommand>(orderCreateEvent);

                command.CreatedAt = DateTime.Now;
                command.TotalPrice = orderCreateEvent.Quantity * orderCreateEvent.Price;
                command.UnitPrice = orderCreateEvent.Price;

                var result = await _mediator.Send(command);
            }
        }

        public void Disconnect()
        {
            _persistentConnection.Dispose();
        }
    }
}
