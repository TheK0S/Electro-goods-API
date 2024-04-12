﻿using Electro_goods_API.Models.Entities;
using Electro_goods_API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Electro_goods_API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributesAdminController : ControllerBase
    {
        private readonly IProductAttributeRepository _context;

        public ProductAttributesAdminController(IProductAttributeRepository context)
        {
            _context = context;
        }

        // GET: api/ProductAttributesAdmin/productId
        [HttpGet("{productId}")]
        public async Task<ActionResult<List<ProductAttribute>>> GetProductAttributesByProductId(int productId)
        {
            return Ok(await _context.GetProductAttributesByProductId(productId));
        }

        // PUT: api/ProductAttributesAdmin/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, ProductAttribute productAttribute)
        {
            if (id != productAttribute.AttributeId)
                return BadRequest();

            await _context.UpdateProductAttribute(id, productAttribute);

            return NoContent();
        }

        // POST: api/ProductAttributesAdmin
        [HttpPost]
        public async Task<ActionResult<ProductAttribute>> PostProductAttribute(ProductAttribute productAttribute)
        {
            await _context.CreateProductAttribute(productAttribute);

            return Created();
        }

        // DELETE: api/ProductAttributesAdmin/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttribute(int id)
        {
            await _context.DeleteProductAttribute(id);

            return NoContent();
        }
    }
}
