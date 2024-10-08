using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contract
{
    public interface IGenricRepository<TEntity,TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        public Task<TEntity?> GetAsync(TKey id);
        public Task AddAsync(TEntity entity);
        public void Update(TEntity entity); 
        public void Delete(TEntity entity);
    }
}
