using Domain.Contracts;
using Domain.Models;
using Presistance.Data;
using Presistance.Repository;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreHubDbContext context;
        private readonly ConcurrentDictionary<string ,object> repo;


        public UnitOfWork(StoreHubDbContext _context)
        {
            context = _context;
            repo = new ConcurrentDictionary<string, object>();
        }
        public IGenericEntity<TEntity, TKey> GetGenericRepo<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        =>(IGenericEntity<TEntity, TKey>) repo.GetOrAdd(typeof(TEntity).Name, new GenericEntityRepo<TEntity , TKey>(context));
       

        public async Task<int> SaveChangeAsync()=>await context.SaveChangesAsync();
      
    }
}
