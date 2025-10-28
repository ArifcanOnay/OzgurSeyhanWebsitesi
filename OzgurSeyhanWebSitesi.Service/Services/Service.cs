using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using OzgurSeyhanWebSitesi.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Service.Services
{
    public class Service<T> : IService<T> where T : BaseEntitiy
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWorks _unitOfWorks;

        public Service(IUnitOfWorks unitOfWorks, IGenericRepository<T> repository)
        {
            _unitOfWorks = unitOfWorks;
            _repository = repository;
        }

        public async Task AddAsync(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            await _repository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();   
        }

        public async Task<int> CountAsync()
        {
           var adet= await _repository.CountAsync();
            return adet;
            
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWorks.CommitAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return  _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);  
        }

        public void Update(T entity)
        {
            _repository.Update(entity); 
            _unitOfWorks.Commit();
        }
    }
}
