using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        public Expression<Func<TEntity , bool>>?Criteria { get; }

        List<Expression<Func<TEntity,object>>> IncludeExpression { get; }

        #region Sorting

        Expression<Func<TEntity, object>>? OrderBy { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        #endregion
        #region Pagination
        public int Take { get; }
        public int Skip { get; }
        public bool IsPagination { get; }
        #endregion
    }
}
