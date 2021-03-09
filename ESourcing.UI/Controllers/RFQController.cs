using ESourcing.Core.Result;
using ESourcing.UI.Clients;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class RFQController : Controller
    {
        public BidClient _bidClient;
        public AuctionClient _auctionClient;
        public RFQController(BidClient bidClient,AuctionClient auctionClient)
        {
            _bidClient = bidClient;
            _auctionClient = auctionClient;
        }
        //TODO:Ertugrula'a not.Vıdeo cekerken yok gıbı davran.
        public async Task<IActionResult> Detail(string id)
        {
            AuctionBidsViewModel model = new AuctionBidsViewModel();

            var auctionResponse = await _auctionClient.GetAuctionById(id);
            var bidsResponse = await _bidClient.GetAllBidsByAuctionId(id);

            model.SellerUserName = HttpContext.User?.Identity.Name;
            model.AuctionId = auctionResponse.Data.Id;
            model.ProductId = auctionResponse.Data.ProductId;
            model.Bids = bidsResponse.Data;

            return View(model);
        }

        [HttpPost]
        public async Task<Result<string>> SendBid(BidViewModel model)
        {
            model.CreatedAt = DateTime.Now;

            var sendBidResponse = await _bidClient.SendBid(model);

            return sendBidResponse;
        }
    }
}
