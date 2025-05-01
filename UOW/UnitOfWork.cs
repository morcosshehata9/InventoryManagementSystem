using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.GenericRepositories;

namespace InventoryManagementSystem.UOW;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext Context;

    public IGenericRepository<Product> Products { get; set; }
    public IGenericRepository<InventoryTransaction> InventoryTransactions {  get; set; }

    public IGenericRepository<Category> Categories { get; set; }

    public IGenericRepository<Warehouse> Warehouses { get; set; }

    public IGenericRepository<ProductWarehouse> ProductWarehouses {  get; set; }

    public UnitOfWork(AppDbContext appDbContext)
    {
        Context = appDbContext;
        Products = new GenericRepository<Product>(Context);
        InventoryTransactions = new GenericRepository<InventoryTransaction>(Context);
        Categories = new GenericRepository<Category>(Context);
        Warehouses = new GenericRepository<Warehouse>(Context);
        ProductWarehouses = new GenericRepository<ProductWarehouse>(Context);
    }
    public async Task<int> SaveAsync()
    {
        return await Context.SaveChangesAsync();
    }
}
