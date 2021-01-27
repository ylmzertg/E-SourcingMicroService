using ESourcing.UI.ApiExtension.Infrastructure;
using ESourcing.UI.ApiExtension.Interfaces;
using ESourcing.UI.Settings;
using ESourcing.UI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ESourcing.UI.ApiExtension
{
    public class ProductApi : BaseHttpClientWithFactory, IProductApi
    {
        private readonly IApiSettings _settings;

        public ProductApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                  .SetPath(_settings.ProductPath)
                                  .HttpMethod(HttpMethod.Post)
                                  .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<ProductViewModel>(message);
        }

        public async Task DeleteProductById(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.ProductPath)
                                .AddToPath(id)
                                .HttpMethod(HttpMethod.Get)
                                .GetHttpMessage();

            await SendRequest<ProductViewModel>(message);
        }

        public async Task<ProductViewModel> GetProduct(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                              .SetPath(_settings.ProductPath)
                              .AddToPath(id)
                              .HttpMethod(HttpMethod.Get)
                              .GetHttpMessage();

            return await SendRequest<ProductViewModel>(message);
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.ProductPath)
                               .HttpMethod(HttpMethod.Get)
                               .GetHttpMessage();

            return await SendRequest<IEnumerable<ProductViewModel>>(message);
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel model)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.ProductPath)
                               .HttpMethod(HttpMethod.Post)
                               .GetHttpMessage();

            var json = JsonConvert.SerializeObject(model);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<ProductViewModel>(message);
        }
    }
}
