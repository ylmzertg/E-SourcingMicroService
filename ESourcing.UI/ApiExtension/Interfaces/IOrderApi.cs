using ESourcing.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderViewModel>> GetOrdersByUserName(string userName);
        Task OrderCreate(OrderCreateCommandViewModel command);
    }
}
