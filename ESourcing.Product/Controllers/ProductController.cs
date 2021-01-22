using ESourcing.Product.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ESourcing.Product.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;
        #endregion

        #region Ctor
        public ProductController(IProductRepository repository, ILogger<ProductController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Crud_Actions

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Entites.Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Entites.Product>>> GetProducts()
        {
            var auctions = await _repository.GetProducts();
            return Ok(auctions);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Entites.Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entites.Product>> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Entites.Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Entites.Product>> CreateProduct([FromBody] Entites.Product product)
        {
            await _repository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Entites.Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Entites.Product value)
        {
            return Ok(await _repository.Update(value));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.Delete(id));
        } 
        #endregion
    }
}
