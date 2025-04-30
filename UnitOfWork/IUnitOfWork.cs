using InventoryManagementSystem.Models;
using InventoryManagementSystem.GenericRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Product> Products { get; }
        IGenericRepository<InventoryTransaction> InventoryTransactions { get; }
        Task<int> SaveAsync(); // int for number of rows affected 
    }
}
