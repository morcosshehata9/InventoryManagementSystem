using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddCategory(AddCategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                await categoryService.AddAsync(category);
                return Ok("Created!");
            }
            return BadRequest("Not Created!!!");
        }
    }
}
