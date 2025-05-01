using InventoryManagementSystem.Data;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IUnitOfWork unitOfWork;

        public InventoryTransactionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }




    }
}
