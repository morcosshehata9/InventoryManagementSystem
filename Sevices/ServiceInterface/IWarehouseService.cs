using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.DTOs.Warehouse;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface IWarehouseService
    {
        Task AddAsync(AddWarehouseDTO warehouse);

    }
}
