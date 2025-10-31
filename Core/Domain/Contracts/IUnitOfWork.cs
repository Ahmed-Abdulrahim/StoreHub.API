using Domain.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangeAsync();
        public IGenericEntity<TEntity, TKey> GetGenericRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>;
    }
}
