using InventoryManagementSystem.DTOs.Product;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductsDTO>> GetAllAsync();
        Task<GetProductsDTO> GetByIdAsync(int id);
        Task AddAsync(AddProductDTO product);
        Task<bool> UpdateAsync(int id, UpdateProductDTO product);
        Task<bool> DeleteAsync(int id);

    }
}
