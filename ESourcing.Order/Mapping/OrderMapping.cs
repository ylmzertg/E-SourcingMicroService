using AutoMapper;
using EventBusRabbitMQ.Events;
using Ordering.Application.Commands;

namespace ESourcing.Order.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
