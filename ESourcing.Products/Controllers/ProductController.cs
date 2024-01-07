using ESourcing.Products.Entities;
using ESourcing.Products.Repositories.Contract;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ESourcing.Products.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProductController : ControllerBase
    {
        #region Variables
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;
        #endregion

        #region Constructor
        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        #endregion

        #region CrudActions

        [HttpGet]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name ="GetProduct")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                _logger.LogError($"Product with id {id} has not been found in mongo database.");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody]Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id}, product);            
        }

        [HttpPut]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {            
            return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType<Product>(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
        #endregion
    }
}
