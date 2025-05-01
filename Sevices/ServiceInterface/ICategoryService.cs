using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.DTOs.Product;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface ICategoryService
    {
        Task AddAsync(AddCategoryDTO category);

    }
}
