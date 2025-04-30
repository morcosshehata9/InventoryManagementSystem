using InventoryManagementSystem.Data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class ProductService : GenericRepository<Product>, IProductService
    {
        private readonly AppDbContext Context;

        public ProductService(AppDbContext appDbContext) : base(appDbContext)
        {
            Context = appDbContext;
        }

    }
}
