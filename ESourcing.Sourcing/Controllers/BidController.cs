using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<BidController> _logger;

        public BidController(
            IBidRepository bidRepository,
            ILogger<BidController> logger)
        {
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SendBid([FromBody] Bid bid)
        {
            await _bidRepository.SendBid(bid);

            return Ok();
        }

        [HttpGet("GetBidsByAuctionId")]
        [ProducesResponseType(typeof(List<Bid>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetBidsByAuctionId(string id)
        {
            IEnumerable<Bid> bids = await _bidRepository.GetBidsByAuctionId(id);

            return Ok(bids);
        }
        
        [HttpGet("GetWinnerBid")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Bid>>> GetWinnerBid(string id)
        {
            Bid bid = await _bidRepository.GetWinnerBid(id);

            return Ok(bid);
        }
    }
}
