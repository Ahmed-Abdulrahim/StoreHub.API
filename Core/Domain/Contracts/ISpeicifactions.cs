using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpeicifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Expression<Func<TEntity, bool>>? Criteria { get; set; }
        List<Expression<Func<TEntity, object>>> AddInclude { get; set; }
        public Expression<Func<TEntity, object>>? OrdederByAsc { get; set; }
        public Expression<Func<TEntity, object>>? OrdederByDesc { get; set; }
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public bool Ispagination { get; set; }
    }
}
