using InventoryManagementSystem.Data;
using InventoryManagementSystem.DTOs.Product;
using InventoryManagementSystem.GenericRepositories;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task AddAsync(AddProductDTO productDTO)
        {
            Product product = new Product
            {
                CategoryId = productDTO.CategoryId,
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                LowStockThreshold = productDTO.LowStockThreshold, 

            };

            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product product = await unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            unitOfWork.Products.Update(product);
            await unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<GetProductsDTO>> GetAllAsync()
        {
            var products = await unitOfWork.Products
               .GetQueryable()
               .Include(p => p.Category)
               .Include(p => p.ProductWarehouses) // ================================
               .Where(p => !p.IsDeleted)
               .ToListAsync();

            var productDTOs = products.Select(p => new GetProductsDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                LowStockThreshold = p.LowStockThreshold,
                Price = p.Price,
                TotalQuantity = p.TotalQuantity,
                CategoryName = p.Category.Name,
            });
            return productDTOs;
        }

        public async Task<GetProductsDTO> GetByIdAsync(int id)
        {
            var product = await unitOfWork.Products
                .GetQueryable()
                .Include(p => p.Category)
                .Include(p => p.ProductWarehouses) // ================================
                .Where(p => !p.IsDeleted && p.Id == id)
                .FirstOrDefaultAsync();

            if (product == null)
                return null;

            return new GetProductsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                LowStockThreshold = product.LowStockThreshold,
                Price = product.Price,
                TotalQuantity = product.TotalQuantity,
                CategoryName = product.Category.Name,

            };


        }

        public async Task<bool> UpdateAsync(int id, UpdateProductDTO productDTO)
        {
            Product product = await unitOfWork.Products.GetByIdAsync(id);
            if (product == null)
            { 
                return false;
            }

            product.Name = productDTO.Name;
            product.Description = productDTO.Description;
            product.Price = productDTO.Price;
            product.LowStockThreshold = productDTO.LowStockThreshold;
            product.IsDeleted = productDTO.IsDeleted;

            unitOfWork.Products.Update(product);
            await unitOfWork.SaveAsync();
            

            return true;
        }
    }
}
