using ESourcing.UI.Clients;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionClient _auctionClient;
        private readonly ProductClient _productClient;

        public AuctionController(AuctionClient auctionClient, ProductClient productClient)
        {
            _auctionClient = auctionClient;
            _productClient = productClient;
        }

        public async Task<IActionResult> Index()
        {
            var auctionList = await _auctionClient.GetAuctions();
            if (auctionList.IsSuccess)
                return View(auctionList.Data);
            else
                return View();
        }

        public async Task<IActionResult> Create()
        {
            //TODO:Burada  urunler lıstelenmelı 
            var productList = await _productClient.GetProducts();
            if (productList.IsSuccess)
                ViewBag.ProductList = productList.Data;

            return View();
            //TODO:hem de 

        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel model)
        {
            var createAuction = await _auctionClient.CreateAuction(model);
            if (createAuction.IsSuccess)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}
