using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.DTOs.Warehouse;
using InventoryManagementSystem.Sevices.ServiceImplementation;
using InventoryManagementSystem.Sevices.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddWarehouse(AddWarehouseDTO warehouseDTO)
        {
            if (ModelState.IsValid)
            {
                await warehouseService.AddAsync(warehouseDTO);
                return Ok("Created!");
            }
            return BadRequest("Not Created!!!");
        }
    }
}
