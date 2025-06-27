using HoangTLM.Core.Repository;
using HoangTLM.Entities.CoreEntities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace HoangTLM.Application
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : AppController<Product_Product>
    {
        public ProductController(IRepository<Product_Product> repository) : base(repository) { }

        [HttpGet("test")]
        [AllowAnonymous]
        public IActionResult Test()
        {
            return Ok(new { message = "API is working without authentication" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Product_Product product)
        {
            await _repository.AddAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Product_Product product)
        {
            if (id != product.Id) return BadRequest();
            await _repository.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _repository.DeleteByIdAsync(id);
            return NoContent();
        }
    }
} 