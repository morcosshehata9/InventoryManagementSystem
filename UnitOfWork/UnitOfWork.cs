using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.GenericRepositories;

namespace InventoryManagementSystem.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext Context;

        public IGenericRepository<Product> Products { get; set; }
        public IGenericRepository<InventoryTransaction> InventoryTransactions {  get; set; }

        public UnitOfWork(AppDbContext appDbContext)
        {
            Context = appDbContext;
            Products = new GenericRepository<Product>(Context);
            InventoryTransactions = new GenericRepository<InventoryTransaction>(Context);
        }
        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
