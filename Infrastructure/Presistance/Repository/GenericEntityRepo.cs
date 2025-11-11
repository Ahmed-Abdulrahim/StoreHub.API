using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repository
{
    public class GenericEntityRepo<TEntity, TKey> : IGenericEntity<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreHubDbContext context;

        public GenericEntityRepo(StoreHubDbContext _context)
        {
            context = _context;
        }
        public async Task AddAsync(TEntity model) => await context.Set<TEntity>().AddAsync(model);


        public void Delete(TEntity model) => context.Set<TEntity>().Remove(model);


        public async Task<IEnumerable<TEntity>> GetAll(bool trackChange = false)
        {
            return trackChange ?
                  await context.Set<TEntity>().ToListAsync()
                 : await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public Task<IEnumerable<TEntity>> GetAllWithSpec(ISpeicifications<TEntity, TKey> spec, bool trackChange = false)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                await context.Set<Product>().Include(p => p.ProductBrands).FirstOrDefaultAsync();
            }
            return await context.Set<TEntity>().FindAsync(id);
        }

        public Task<TEntity> GetByIdWithSpec(ISpeicifications<TEntity, TKey> spec)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity model) => context.Set<TEntity>().Update(model);

    }
}
