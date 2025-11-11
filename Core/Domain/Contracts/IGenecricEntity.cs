using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericEntity<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll(bool trackChange = false);
        Task<IEnumerable<TEntity>> GetAllWithSpec(ISpeicifications<TEntity, TKey> spec, bool trackChange = false);
        Task<TEntity> GetById(TKey id);
        Task<TEntity> GetByIdWithSpec(ISpeicifications<TEntity, TKey> spec);
        Task AddAsync(TEntity model);
        void Update(TEntity model);
        void Delete(TEntity model);
    }
}
