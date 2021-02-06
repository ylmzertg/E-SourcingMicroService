using AutoMapper;
using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
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
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionController> _logger;
        //private readonly EventBusRabbitMQProducer _eventBus;
        private readonly IMapper _mapper;

        public AuctionController(
            IAuctionRepository auctionRepository,
            IBidRepository bidRepository,
            //EventBusRabbitMQProducer eventBus,
            IMapper mapper,
            ILogger<AuctionController> logger)
        {
            _auctionRepository = auctionRepository ?? throw new ArgumentNullException(nameof(auctionRepository));
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            //_eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetAuctions()
        {
            var auctions = await _auctionRepository.GetAuctions();
            return Ok(auctions);
        }

        [HttpGet("{id:length(24)}", Name = "GetAuction")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var auctions = await _auctionRepository.GetAuction(id);

            if (auctions == null)
            {
                _logger.LogError($"Auction with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(auctions);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.Create(auction);

            return CreatedAtRoute("GetAuction", new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAuction([FromBody] Auction value)
        {
            return Ok(await _auctionRepository.Update(value));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAuctionById(string id)
        {
            return Ok(await _auctionRepository.Delete(id));
        }

        [HttpPost("CompleteAuction/{id:length(24)}")]
        [ProducesResponseType(typeof(Bid), (int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Auction>> CompleteAuction(string id)
        {
            Auction auction = await _auctionRepository.GetAuction(id);
            if(auction == null)
                return NotFound();

            if(auction.Status != (int)Status.Active)
            {
                _logger.LogError("Auction can not completed");
                return BadRequest();
            }

            Bid bid = await _bidRepository.GetWinnerBid(id);
            if (bid == null)
                return NotFound();

            OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity = auction.Quantity;

            auction.Status = (int)Status.Closed;
            bool updateResp = await _auctionRepository.Update(auction);
            if(!updateResp)
            {
                _logger.LogError("Auction can not updated");
                return BadRequest();
            }

            try
            {
                //_eventBus.PublishEvent(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "Sourcing");
                throw;
            }

            return Accepted();
        }

        [HttpPost("TestEvent")]
        public ActionResult<Auction> TestEvent(string id)
        {
            OrderCreateEvent eventMessage = new OrderCreateEvent();
            eventMessage.AuctionId = "600a800fee404a87fb9f148e";
            eventMessage.ProductId = "600a800fee404a87fb9f148e";
            eventMessage.Price = 11;
            eventMessage.Quantity = 13;
            eventMessage.SellerUserName = "test@test.com";

            try
            {
                //_eventBus.PublishEvent(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.RequestId, "Sourcing");
                throw;
            }

            return Accepted();
        }
    }
}
