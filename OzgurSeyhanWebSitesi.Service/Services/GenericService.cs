using OzgurSeyhanWebSitesi.Core.Models;
using OzgurSeyhanWebSitesi.Core.Repositories;
using OzgurSeyhanWebSitesi.Core.Services;
using OzgurSeyhanWebSitesi.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OzgurSeyhanWebSitesi.Bussinies.Services
{
    public class GenericService<T> : IGenericService<T> where T : BaseEntitiy
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWorks _unitOfWorks;
        public GenericService(IGenericRepository<T> repository, IUnitOfWorks unitOfWorks)
        {
            _repository = repository;
            _unitOfWorks = unitOfWorks;
        }


        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWorks.CommitAsync();
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
            _unitOfWorks.Commit();
        }

        public List<T> GetAll()
        {
            return _repository.GetAll();
        }

        public T GetByIdAsync(int id)
        {
            var value = _repository.GetById(id);
            return value;
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _unitOfWorks.Commit();
        }
    }
}
