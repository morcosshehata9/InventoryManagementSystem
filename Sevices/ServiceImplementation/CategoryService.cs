using InventoryManagementSystem.DTOs.Category;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Sevices.ServiceInterface;
using InventoryManagementSystem.UOW;

namespace InventoryManagementSystem.Sevices.ServiceImplementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task AddAsync(AddCategoryDTO categoryDTO)
        {
            Category category = new Category
            {
                Name = categoryDTO.Name,
            };

            await unitOfWork.Categories.AddAsync(category);
            await unitOfWork.SaveAsync();
        }
    }
}
