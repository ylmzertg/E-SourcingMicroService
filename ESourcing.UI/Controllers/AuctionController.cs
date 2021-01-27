using ESourcing.Core.Repositories;
using ESourcing.UI.Clients;
using ESourcing.UI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ESourcing.UI.Controllers
{
    public class AuctionController : Controller
    {
        private readonly AuctionClient _auctionClient;
        private readonly ProductClient _productClient;
        private readonly IUserRepository _userRepository;

        public AuctionController(AuctionClient auctionClient, ProductClient productClient, IUserRepository userRepository)
        {
            _auctionClient = auctionClient;
            _productClient = productClient;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var productList = await _productClient.GetProducts();
            if (productList.IsSuccess)
                ViewBag.ProductList = productList.Data;
            var userList = await _userRepository.GetAllAsync();
            ViewBag.UserList = userList;
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

            var userList = await _userRepository.GetAllAsync();
            ViewBag.UserList = userList;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionViewModel model)
        {
            model.Status = 1;
            model.CreatedAt = DateTime.Now;
            var createAuction = await _auctionClient.CreateAuction(model);
            if (createAuction.IsSuccess)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}
