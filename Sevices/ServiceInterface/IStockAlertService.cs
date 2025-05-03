using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Sevices.ServiceInterface
{
    public interface IStockAlertService
    {
        Task CheckAndNotifyAsync(Product product);
    }
}
