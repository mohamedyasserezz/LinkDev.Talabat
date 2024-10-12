using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repository
{
    internal static class SpecificationEvaluator<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> query, ISpecifications<TEntity, TKey> specifications)
        {
            if (specifications.Criteria is not null)
                query = query.Where(specifications.Criteria);

            if(specifications.OrderByDesc is not null)
                query = query.OrderByDescending(specifications.OrderByDesc);
            else if(specifications.OrderBy is not null)
                query = query.OrderBy(specifications.OrderBy);

            if(specifications.IsPaginationEnabled is true)
                query = query.Skip(specifications.Skip).Take(specifications.Take);

            if (specifications.Includes is not null)
            {
                query = specifications.Includes.Aggregate(query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            }

            return query;
        }
    }
}
