using InventoryManagementSystem.Data;
using InventoryManagementSystem.GenericRepositories;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.GenericRepositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext Context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext appDbContext)
        {
            Context = appDbContext;
            _dbSet = Context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
        }
        public void Update(T obj)
        {
            _dbSet.Update(obj);
        }

        public void Delete(T obj)
        {
            _dbSet.Remove(obj); 
        }

    }
}
