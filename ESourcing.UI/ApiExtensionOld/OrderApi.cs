using ESourcing.UI.ApiExtension.Infrastructure;
using ESourcing.UI.ApiExtension.Interfaces;
using ESourcing.UI.Settings;
using ESourcing.UI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension
{
    public class OrderApi : BaseHttpClientWithFactory, IOrderApi
    {
        private readonly IApiSettings _settings;

        public OrderApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrdersByUserName(string userName)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                 .SetPath(_settings.OrderPath)
                 .AddToPath(userName)
                 .HttpMethod(HttpMethod.Get)
                 .GetHttpMessage();

            return await SendRequest<IEnumerable<OrderViewModel>>(message);
        }

        public async Task OrderCreate(OrderCreateCommandViewModel command)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                              .SetPath(_settings.OrderPath)
                              .HttpMethod(HttpMethod.Post)
                              .GetHttpMessage();

            var json = JsonConvert.SerializeObject(command);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            await SendRequest<OrderCreateCommandViewModel>(message);
        }
    }
}
