using Microsoft.AspNetCore.Mvc;
using SimpleStoreFront.Data;
using SimpleStoreFront.Data.Entities;

namespace SimpleStoreFront.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly IStoreFrontRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IStoreFrontRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllProducts());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get products: {ex}");
                return BadRequest("Failed to get products");
            }
            
        }
    }
}
