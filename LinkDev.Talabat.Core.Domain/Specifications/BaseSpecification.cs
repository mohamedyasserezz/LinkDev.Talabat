﻿using LinkDev.Talabat.Core.Domain.Contract;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes {  get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null;
        public Expression<Func<TEntity,object>>? OrderByDesc { get; set; } = null;
        public int Take {  get; set; }
        public int Skip {  get; set; }
        public bool IsPaginationEnabled { get; set; }

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<TEntity, bool>>? expression)
        {
            Criteria = expression;
        }
        public BaseSpecification(TKey id)
        {
            Criteria = E => E.Id.Equals(id);
        }
        private protected virtual void AddIncludes()
        {
           
        }
        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>>? orderBy)
        {
            OrderBy = orderBy;
        }
        private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }
        private protected virtual void ApplyPagination(int skip, int take)
        {
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
