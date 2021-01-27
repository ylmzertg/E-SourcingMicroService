using ESourcing.Core.Common;
using ESourcing.Core.Result;
using ESourcing.UI.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ESourcing.UI.Clients
{
    public class AuctionClient
    {
        public HttpClient _client { get; }
        public AuctionClient(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(CommonInfo.LocalAuctionBaseAddress);
        }

        public async Task<Result<List<AuctionViewModel>>> GetAuctions()
        {
            //List<AuctionViewModel> returnData = new List<AuctionViewModel>();
            var response = await _client.GetAsync("/api/v1/Auction");
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<AuctionViewModel>>(responseData);
                if (result.Any())
                    return new Result<List<AuctionViewModel>>(true, ResultConstant.RecordFound, result.ToList());
                return new Result<List<AuctionViewModel>>(false, ResultConstant.RecordNotFound);
            }
            return new Result<List<AuctionViewModel>>(false, ResultConstant.RecordNotFound);
        }
    }
}
