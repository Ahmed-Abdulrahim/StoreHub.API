using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpeicifications<TEntity, Tkey> spec)
            where TEntity : BaseEntity<Tkey>
        {
            var query = inputQuery;
            if (spec.Criteria is not null)
            {
                query = query.Where(spec.Criteria);
            }
            if (spec.OrdederByAsc is not null)
            {
                query = query.OrderBy(spec.OrdederByAsc);
            }
            else if (spec.OrdederByDesc is not null)
            {
                query = query.OrderByDescending(spec.OrdederByDesc);
            }

            query = spec.AddInclude.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
