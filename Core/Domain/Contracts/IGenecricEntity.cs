using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericEntity <TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll(bool trackChange =false);
        Task<TEntity> GetById(TKey id);
        Task AddAsync(TEntity model);
        void Update(TEntity model);
        void Delete(TEntity model); 
    }
}
