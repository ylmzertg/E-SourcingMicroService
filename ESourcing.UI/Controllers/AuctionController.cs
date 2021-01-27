using ESourcing.UI.Clients;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionClient _auctionClient;

        public AuctionController(AuctionClient auctionClient)
        {
            _auctionClient = auctionClient;
        }

        public async Task<IActionResult> Index()
        {
            var auctionList = await _auctionClient.GetAuctions();
            if (auctionList.IsSuccess)
                return View(auctionList.Data);
            else
                return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
