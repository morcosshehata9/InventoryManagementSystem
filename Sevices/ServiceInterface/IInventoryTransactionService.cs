using InventoryManagementSystem.DTOs.InventoryTransaction;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface IInventoryTransactionService
    {
        Task<bool> AddStock(AddStockDTO addStockDTO, string PerformedBy);
        Task<bool> DeleteStock(RemoveStockDTO removeStockDTO, string PerformedBy);
        Task<bool> TransferStock(TransferStockDTO transferStockDTO, string PerformedBy);


    }
}
