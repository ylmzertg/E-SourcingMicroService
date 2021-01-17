using ESourcing.Sourcing.Entities;
using ESourcing.Sourcing.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Sourcing.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SourcingController : ControllerBase
    {
        private readonly ISourcingRepository _repository;
        private readonly ILogger<SourcingController> _logger;

        public SourcingController(ISourcingRepository repository, ILogger<SourcingController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Auction>>> GetProducts()
        {
            var products = await _repository.GetAuctions();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetAuction")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Auction>> GetAuction(string id)
        {
            var product = await _repository.GetAuction(id);

            if (product == null)
            {
                _logger.LogError($"Auction with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(product);
        }

        //[Route("[action]/{category}")]
        //[HttpGet]
        //[ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<Auction>>> GetProductByCategory(string category)
        //{
        //    var product = await _repository.GetProductByCategory(category);
        //    return Ok(product);
        //}

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Auction>> CreateAuction([FromBody] Auction product)
        {
            await _repository.Create(product);

            return CreatedAtRoute("GetAuction", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Auction value)
        {
            return Ok(await _repository.Update(value));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAuctionById(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
