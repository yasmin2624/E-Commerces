using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Entities.ProductModules;

namespace Service.Specifications
{
    public static class SpecificationEvaluator
    {
        //Create Query
        //dbContext.Products.Where(P => P.Id == id).Include(P => P.ProductType).Include(P => P.ProductBrand);

        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            // Sum = Sum+i
            var Query = inputQuery;
            if (specification.Criteria is not null)
            {
                Query = Query.Where(specification.Criteria);
            }

            if (specification.OrderBy is not null)
            {
                Query = Query.OrderBy(specification.OrderBy);
            }  
            if (specification.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.IncludeExpression is not null && specification.IncludeExpression.Count > 0)
            {
                Query = specification.IncludeExpression
                    .Aggregate(Query, (current, includeExpression) => current.Include(includeExpression));
            }
            if (specification.IsPagination)
            {
                Query = Query.Skip(specification.Skip).Take(specification.Take);
            }

            return Query;   
        }

    }
}
