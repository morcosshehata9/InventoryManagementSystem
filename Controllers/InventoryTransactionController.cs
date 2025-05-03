using System.Threading.Tasks;
using InventoryManagementSystem.DTOs.InventoryTransaction;
using InventoryManagementSystem.Sevices.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactionService inventoryTransactionService;

        public InventoryTransactionController(IInventoryTransactionService inventoryTransactionService)
        {
            this.inventoryTransactionService = inventoryTransactionService;
        }

        // add stock (increase quantity of specific product)


        [HttpPost("AddStock")]
        public async Task<IActionResult> IncreaseProductStock([FromBody] AddStockDTO addStockDTO)
        {
            var performedBy = User.Identity.Name;

            bool successAdding = await inventoryTransactionService.AddStock(addStockDTO, performedBy);
            if (!successAdding) {
                return BadRequest("Failed to increase quantity!");
            }
            return Ok("Product quantity increased successfully!");
        }
        // remove stock 


        [HttpPost("RemoveStock")]
        public async Task<IActionResult> DecreaseProductStock([FromBody] RemoveStockDTO removeStockDTO)
        {
            var performedBy = User.Identity.Name;

            bool successRemoving = await inventoryTransactionService.DeleteStock(removeStockDTO, performedBy);
            if (!successRemoving)
            {
                return BadRequest("Failed to decrease quantity!");
            }
            return Ok("Product quantity decreased successfully!");
        }
        // transfare stock between warehouse

        [HttpPost("TransferStock")]

        public async Task<IActionResult> TransferProductStock([FromBody] TransferStockDTO transferStockDTO)
        {
            var performedBy = User.Identity.Name;

            bool successTransfering = await inventoryTransactionService.TransferStock(transferStockDTO, performedBy);
            if (!successTransfering)
            {
                return BadRequest("Failed to tranfer quantity!");
            }
            return Ok("Product quantity transfered successfully!");
        }




    }
}
