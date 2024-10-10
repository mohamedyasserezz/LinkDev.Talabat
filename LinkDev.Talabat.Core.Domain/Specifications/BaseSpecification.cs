using LinkDev.Talabat.Core.Domain.Contract;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Specifications
{
    public class BaseSpecification<TEntity, TKey> : ISpecifications<TEntity, TKey>
         where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; } = null;
        public List<Expression<Func<TEntity, object>>> Includes {  get; set; } = new List<Expression<Func<TEntity, object>>>();
        public BaseSpecification()
        {
        }
        public BaseSpecification(TKey id)
        {
            Criteria = E => E.Id.Equals(id);
        }
    }
}
