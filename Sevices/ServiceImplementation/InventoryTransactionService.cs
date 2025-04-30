using InventoryManagementSystem.Data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UnitOfWork;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class InventoryTransactionService : GenericRepository<InventoryTransaction>, IInventoryTransactionService
    {
        private readonly AppDbContext Context;

        public InventoryTransactionService(AppDbContext appDbContext) : base(appDbContext)
        {
            Context = appDbContext;
        }

    }
}
