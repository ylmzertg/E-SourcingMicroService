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
    public class BidApi : BaseHttpClientWithFactory, IBidApi
    {
        private readonly IApiSettings _settings;

        public BidApi(IHttpClientFactory factory, IApiSettings settings)
            : base(factory)
        {
            _settings = settings;
        }

        public async Task<IEnumerable<BidViewModel>> GetBidsByAuctionId(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                            .SetPath(_settings.BidPath)
                            .AddToPath(id)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<IEnumerable<BidViewModel>>(message);
        }

        public async Task<IEnumerable<BidViewModel>> GetWinnerBid(string id)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                            .SetPath(_settings.BidPath)
                            .AddToPath(id)
                            .HttpMethod(HttpMethod.Get)
                            .GetHttpMessage();

            return await SendRequest<IEnumerable<BidViewModel>>(message);
        }

        public async Task SendBid(BidViewModel bidModel)
        {
            var message = new HttpRequestBuilder(_settings.BaseAddress)
                                 .SetPath(_settings.BidPath)
                                 .HttpMethod(HttpMethod.Post)
                                 .GetHttpMessage();

            var json = JsonConvert.SerializeObject(bidModel);
            message.Content = new StringContent(json, Encoding.UTF8, "application/json");

            await SendRequest<ProductViewModel>(message);
        }
    }
}
