using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.DTOs.Warehouse;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork unitOfWork;

        public WarehouseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task AddAsync(AddWarehouseDTO addWarehouseDTO)
        {
            Warehouse warehouse = new Warehouse
            {
                Name = addWarehouseDTO.Name,
                Location = addWarehouseDTO.Location
            };

            await unitOfWork.Warehouses.AddAsync(warehouse);
            await unitOfWork.SaveAsync();
        }
    }
}
