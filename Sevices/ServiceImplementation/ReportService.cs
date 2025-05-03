using InventoryManagementSystem.DTOs.Reports;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<LowStockReportDTO>> GetLowStockProductsAsync()
        {
            var lowStockProducts = await unitOfWork.Products
                .GetQueryable()
                .AsNoTracking()
                .Include(p => p.Category)
                .Include(p => p.ProductWarehouses)
                .Where(p => p.LowStockThreshold > 0)
                .Select(p => new
                {
                    Product = p,
                    TotalQuantity = p.ProductWarehouses.Sum(pw => pw.Quantity)
                })
                .Where(p => p.TotalQuantity < p.Product.LowStockThreshold)
                .Select(p => new LowStockReportDTO
                {
                    ProductId = p.Product.Id,
                    ProductName = p.Product.Name,
                    Quantity = p.TotalQuantity,
                    LowStockThreshold = p.Product.LowStockThreshold,
                    CategoryName = p.Product.Category.Name,
                })
                .ToListAsync();

            return lowStockProducts;
        }

        public async Task<List<TransactionHistoryDTO>> GetTransactionsHistoryAsync(TransactionHistoryFilterDTO filterDTO)
        {
            var query = unitOfWork.InventoryTransactions
                .GetQueryable()
                .AsNoTracking();

            // filters customization 

            // by ProductId
            if (filterDTO.ProductId.HasValue)
                query = query.Where(p => p.ProductId == filterDTO.ProductId);
            
            // by CategoryId
            if (filterDTO.CategoryId.HasValue)
                query = query.Where(p => p.Product.CategoryId == filterDTO.CategoryId);

            // by TransactionType
            if (filterDTO.TransactionType.HasValue)
                query = query.Where(p => p.TransactionType == filterDTO.TransactionType);

            // by Date
            if (filterDTO.DateFrom.HasValue)
                query = query.Where(p => p.TransactionDate >= filterDTO.DateFrom);

            if (filterDTO.DateTo.HasValue)
                query = query.Where(p => p.TransactionDate <= filterDTO.DateTo);

            // by user
            if (!string.IsNullOrEmpty(filterDTO.PerformedBy))
                query = query.Where(p => p.PerformedBy == filterDTO.PerformedBy);

            var history = await query
                .Select(it => new TransactionHistoryDTO
                {
                    TransactionId = it.Id,
                    ProductId = it.ProductId,
                    ProductName = it.Product.Name,
                    Quantity = it.Quantity,
                    CategoryName = it.Product.Category.Name,
                    TransactionType = it.TransactionType.ToString(),
                    TransactionDate = it.TransactionDate,
                    PerformedBy = it.PerformedBy,
                    SourceWarehouse = it.SourceWarehouse.Name,
                    DestinationWarehouse = it.DestinationWarehouse.Name,
                })
                .ToListAsync();

            return history;
        }

    }
}
