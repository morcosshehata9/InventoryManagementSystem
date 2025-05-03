using System.Threading.Tasks;
using InventoryManagementSystem.DTOs.Reports;
using InventoryManagementSystem.Sevices.ServiceInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet("LowStock")]
        public async Task<IActionResult> GetLowStockProducts()
        { 
              var products = await reportService.GetLowStockProductsAsync();
              return Ok(products);
        }

        [HttpPost("History")]
        public async Task<IActionResult> GetTransactionHistory([FromBody] TransactionHistoryFilterDTO transactionHistoryFilterDTO)
        {
            var transactions = await reportService.GetTransactionsHistoryAsync(transactionHistoryFilterDTO);
            return Ok(transactions);
        }    
    }
}
