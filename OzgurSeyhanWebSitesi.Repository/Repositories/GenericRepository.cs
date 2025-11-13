using Microsoft.EntityFrameworkCore;
using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
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
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
            _dbSet=_context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);  
        }

        public void Delete(int id)
        {
            var silinecekDeger =GetById(id);
            _dbSet.Remove(silinecekDeger);
          
            
          
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
            
        }

        public T GetById(int id)
        {
           var value=_dbSet.ToList().FirstOrDefault(x=>x.Id==id);
            return value;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);

        }

       
    }
}
