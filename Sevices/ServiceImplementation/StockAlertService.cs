using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class StockAlertService : IStockAlertService
    {
        private readonly IUnitOfWork unitOfWork;

        public StockAlertService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public Task CheckAndNotifyAsync(Product product)
        {
            if (product.LowStockThreshold>0 && product.TotalQuantity < product.LowStockThreshold)
            {
                Console.WriteLine($" ALERT: Product '{product.Name}' is below its stock threshold!!!");

            }
            return Task.CompletedTask;
        }
    }
}
