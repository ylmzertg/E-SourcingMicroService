using ESourcing.UI.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension.Interfaces
{
    public interface IOrderApi
    {
        Task<IEnumerable<OrderViewModel>> GetOrdersByUserName(string userName);
        Task OrderCreate(OrderCreateCommandViewModel command);
    }
}
