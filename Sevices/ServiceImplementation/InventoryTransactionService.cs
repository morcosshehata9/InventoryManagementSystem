using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTOs.InventoryTransaction;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IStockAlertService stockAlertService;

        public InventoryTransactionService(IUnitOfWork unitOfWork, IStockAlertService stockAlertService)
        {
            this.unitOfWork = unitOfWork;
            this.stockAlertService = stockAlertService;
        }

        public async Task<bool> AddStock(AddStockDTO addStockDTO, string performedBy)
        {
            Product product = await unitOfWork.Products.GetByIdAsync(addStockDTO.ProductId);
            Warehouse warehouse = await unitOfWork.Warehouses.GetByIdAsync(addStockDTO.WarehouseId);
            if (product == null || warehouse == null)
            { 
                return false;
            }

            var productWarehouse = await unitOfWork.ProductWarehouses
                .GetQueryable()
                .FirstOrDefaultAsync(pw => pw.ProductId == addStockDTO.ProductId && pw.WarehouseId == addStockDTO.WarehouseId);
           
            if (productWarehouse == null)
            {
                productWarehouse = new ProductWarehouse
                {
                    ProductId = addStockDTO.ProductId,
                    Quantity = addStockDTO.Quantity,
                    WarehouseId = addStockDTO.WarehouseId
                };
                await unitOfWork.ProductWarehouses.AddAsync(productWarehouse);
            }
            else
            {
                productWarehouse.Quantity += addStockDTO.Quantity;
                unitOfWork.ProductWarehouses.Update(productWarehouse);
                
            }

            var newTransaction = new InventoryTransaction
            {
                ProductId = product.Id,
                TransactionDate = DateTime.Now,
                TransactionType = Enums.TransactionType.AddStock,
                Quantity = addStockDTO.Quantity,
                PerformedBy = performedBy,
                SourceWarehouseId = warehouse.Id,
                DestinationWarehouseId = warehouse.Id
            };

            await unitOfWork.InventoryTransactions.AddAsync(newTransaction);
            await unitOfWork.SaveAsync();

            await stockAlertService.CheckAndNotifyAsync(product);
            return true;
        }

        public async Task<bool> DeleteStock(RemoveStockDTO removeStockDTO, string performedBy)
        {
            Product product = await unitOfWork.Products.GetByIdAsync(removeStockDTO.ProductId);
            Warehouse warehouse = await unitOfWork.Warehouses.GetByIdAsync(removeStockDTO.WarehouseId);
            if (product == null || warehouse == null)
            {
                return false;
            }

            var productWarehouse = await unitOfWork.ProductWarehouses
                .GetQueryable()
                .FirstOrDefaultAsync(pw => pw.ProductId == removeStockDTO.ProductId && pw.WarehouseId == removeStockDTO.WarehouseId);


            if (productWarehouse == null || productWarehouse.Quantity < removeStockDTO.Quantity)
            {
                return false; // quantity not enough to decrease
            }

            productWarehouse.Quantity -= removeStockDTO.Quantity;
            unitOfWork.ProductWarehouses.Update(productWarehouse);



            var newTransaction = new InventoryTransaction
            {
                ProductId = product.Id,
                TransactionDate = DateTime.Now,
                TransactionType = Enums.TransactionType.RemoveStock,
                Quantity = removeStockDTO.Quantity,
                PerformedBy = performedBy,
                SourceWarehouseId = warehouse.Id,
                DestinationWarehouseId = warehouse.Id
            };

            await unitOfWork.InventoryTransactions.AddAsync(newTransaction);
            await unitOfWork.SaveAsync();

            await stockAlertService.CheckAndNotifyAsync(product);
            return true;
        }

        public async Task<bool> TransferStock(TransferStockDTO transferStockDTO, string performedBy)
        {
            Product product = await unitOfWork.Products.GetByIdAsync(transferStockDTO.ProductId);
            Warehouse sourceWarehouse = await unitOfWork.Warehouses.GetByIdAsync(transferStockDTO.SourceWarehouseId);
            Warehouse DestinationWarehouse = await unitOfWork.Warehouses.GetByIdAsync(transferStockDTO.DestinationWarehouseId);

            if (product == null || sourceWarehouse == null || DestinationWarehouse == null)
            {
                return false;
            }

            var sourceProductWarehouse = await unitOfWork.ProductWarehouses
                .GetQueryable()
                .FirstOrDefaultAsync(spw=> spw.ProductId == transferStockDTO.ProductId &&  spw.WarehouseId == transferStockDTO.SourceWarehouseId);

            if (sourceProductWarehouse == null || sourceProductWarehouse.Quantity < transferStockDTO.Quantity)
            {
                return false;
            }

            var destinationProductWarehouse = await unitOfWork.ProductWarehouses
                .GetQueryable()
                .FirstOrDefaultAsync(spw => spw.ProductId == transferStockDTO.ProductId && spw.WarehouseId == transferStockDTO.DestinationWarehouseId);

            if (destinationProductWarehouse == null)
            {
                destinationProductWarehouse = new ProductWarehouse
                {
                    ProductId = transferStockDTO.ProductId,
                    Quantity = transferStockDTO.Quantity,
                    WarehouseId = transferStockDTO.DestinationWarehouseId
                };

                await unitOfWork.ProductWarehouses.AddAsync(destinationProductWarehouse);
            }
            else
            {
                destinationProductWarehouse.Quantity += transferStockDTO.Quantity;
                unitOfWork.ProductWarehouses.Update(destinationProductWarehouse);
            }

            sourceProductWarehouse.Quantity -= transferStockDTO.Quantity;
            unitOfWork.ProductWarehouses.Update(sourceProductWarehouse);



            var newTransaction = new InventoryTransaction
            {
                ProductId = product.Id,
                TransactionDate = DateTime.Now,
                TransactionType = Enums.TransactionType.TransferStock,
                Quantity = transferStockDTO.Quantity,
                PerformedBy = performedBy,
                SourceWarehouseId = transferStockDTO.SourceWarehouseId,
                DestinationWarehouseId = transferStockDTO.DestinationWarehouseId
            };

            await unitOfWork.InventoryTransactions.AddAsync(newTransaction);
            await unitOfWork.SaveAsync();

            await stockAlertService.CheckAndNotifyAsync(product);
            return true;
        }
    }
}
