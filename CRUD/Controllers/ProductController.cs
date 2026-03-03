using Microsoft.AspNetCore.Mvc;
using CRUD.Interfaces;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductRequest request)
        {
            try
            {
                var created = _service.Create(
                    request.Id,
                    request.Name,
                    request.Price,
                    request.Stock
                );

                if (!created)
                    return Conflict("Product already exists.");

                return Created("", null);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _service.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var product = _service.GetById(id);

            if (product is null)
                return NotFound();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] ProductRequest request)
        {
            try
            {
                var updated = _service.Update(
                    id,
                    request.Name,
                    request.Price,
                    request.Stock
                );

                if (!updated)
                    return NotFound();

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var deleted = _service.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }

    public record ProductRequest(
        string Id,
        string Name,
        decimal Price,
        int Stock
    );
}