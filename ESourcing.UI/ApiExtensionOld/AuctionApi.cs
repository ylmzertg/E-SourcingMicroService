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
    public class AuctionApi : BaseHttpClientWithFactory, IAuctionApi
    {
        private readonly IApiSettings _settings;

        public AuctionApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<AuctionViewModel> CompleteAuction(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                              .SetPath(_settings.AuctionPath)
                              .AddToPath(id)
                              .HttpMethod(HttpMethod.Get)
                              .GetHttpMessage();

            return await SendRequest<AuctionViewModel>(message);
        }

        public async Task<AuctionViewModel> CreateAuction(AuctionViewModel productModel)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                  .SetPath(_settings.AuctionPath)
                                  .HttpMethod(HttpMethod.Post)
                                  .GetHttpMessage();

            var json = JsonConvert.SerializeObject(productModel);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return await SendRequest<AuctionViewModel>(message);
        }

        public async Task DeleteAuctionById(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.AuctionPath)
                                .AddToPath(id)
                                .HttpMethod(HttpMethod.Get)
                                .GetHttpMessage();

            await SendRequest<AuctionViewModel>(message);
        }

        public async Task<AuctionViewModel> GetAuction(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                             .SetPath(_settings.AuctionPath)
                             .AddToPath(id)
                             .HttpMethod(HttpMethod.Get)
                             .GetHttpMessage();

            return await SendRequest<AuctionViewModel>(message);
        }

        public async Task<IEnumerable<AuctionViewModel>> GetAuctions()
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                .SetPath(_settings.AuctionPath)
                                .HttpMethod(HttpMethod.Get)
                                .GetHttpMessage();

            return await SendRequest<IEnumerable<AuctionViewModel>>(message);
        }

        public async Task UpdateAuction(AuctionViewModel value)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                               .SetPath(_settings.ProductPath)
                               .HttpMethod(HttpMethod.Post)
                               .GetHttpMessage();

            var json = JsonConvert.SerializeObject(value);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

             await SendRequest<AuctionViewModel>(message);
        }
    }
}
