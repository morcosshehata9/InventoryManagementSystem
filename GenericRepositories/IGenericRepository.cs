using InventoryManagementSystem.DTOs.Product;

namespace InventoryManagementSystem.GenericRepositories
{
    public interface IGenericRepository<T>  where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T obj);
        void Update(T obj);
        Task<bool> DeleteAsync(int id);
        IQueryable<T> GetQueryable();
    }
}
