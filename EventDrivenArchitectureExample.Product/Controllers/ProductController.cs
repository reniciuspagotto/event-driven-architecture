using EventDrivenArchitectureExample.Product.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EventDrivenArchitectureExample.Product.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductHandler _productHandler;

        public ProductController(IProductHandler productHandler)
        {
            _productHandler = productHandler;
        }

        [HttpPost("product")]
        public async Task<IActionResult> CreateProduct(Data.Entities.Product product)
        {
            var response = await _productHandler.Create(product);
            return Ok(response);
        }
    }
}
