using InventoryManagementSystem.DTOs.Reports;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface IReportService
    {
        Task<List<LowStockReportDTO>> GetLowStockProductsAsync();
        Task<List<TransactionHistoryDTO>> GetTransactionsHistoryAsync(TransactionHistoryFilterDTO transactionHistoryFilterDTO);

    }
}
