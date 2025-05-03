using InventoryManagementSystem.Models;
using InventoryManagementSystem.GenericRepositories;

namespace InventoryManagementSystem.UOW;

public interface IUnitOfWork
{
    IGenericRepository<Product> Products { get; }
    IGenericRepository<InventoryTransaction> InventoryTransactions { get; }
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<Warehouse> Warehouses { get; }
    IGenericRepository<ProductWarehouse> ProductWarehouses { get; }

    


    Task<int> SaveAsync(); // int for number of rows affected 
}
