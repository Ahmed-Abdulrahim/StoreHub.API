using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Speicifications
{
    public class BaseSpeicification<TEntity, Tkey> : ISpeicifications<TEntity, Tkey>
        where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> AddInclude { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public BaseSpeicification(Expression<Func<TEntity, bool>> expression)
        {
            Criteria = expression;
        }
        public void GetInclude(Expression<Func<TEntity, object>> expression)
        {
            AddInclude.Add(expression);
        }

    }
}
