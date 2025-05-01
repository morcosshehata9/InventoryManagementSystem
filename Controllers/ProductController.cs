using System.Threading.Tasks;
using InventoryManagementSystem.DTOs.Product;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }



        [HttpGet("All")]
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<GetProductsDTO> products = await _productService.GetAllAsync();
            return Ok(products);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id) {
            GetProductsDTO product = await _productService.GetByIdAsync(id);
            if (product == null) {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductDTO addProductDTO) {
            if (ModelState.IsValid)
            {
                await _productService.AddAsync(addProductDTO);
                return NoContent();

            }
            return BadRequest(ModelState);
        }

        [HttpPut("Update/{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            bool UpdatedSuccessfully = await _productService.UpdateAsync(id, productDTO);
            if (!UpdatedSuccessfully) { 
                return NotFound("Product Not found!");
            }
            

            return Ok("Updated Successfully!");

        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> DeleteProduct(int id) {

            GetProductsDTO product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(id);
            return Ok();


        }

    }





}
// Add product
// update 
// delete
// getProductDetails
// list of all products