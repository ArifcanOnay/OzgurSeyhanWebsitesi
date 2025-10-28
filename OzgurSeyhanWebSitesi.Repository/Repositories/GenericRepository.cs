using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntitiy
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbset;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbset= _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbset.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
           return await _dbset.CountAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {

           var result= await _dbset.FindAsync(id);
            if(result != null)
            {
                _dbset.Remove(result);
            }
            return true;

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return  await _dbset.ToListAsync();

            

        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbset.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _dbset.Update(entity);
        }
    }
}
