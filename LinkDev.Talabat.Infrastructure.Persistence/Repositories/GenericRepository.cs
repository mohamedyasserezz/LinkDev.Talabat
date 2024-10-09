using LinkDev.Talabat.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.Repositories
{
    internal class GenericRepository<TEntity, TKey>(StoreContext _storeContext) : IGenricRepository<TEntity, TKey>
          where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
            if(typeof(TEntity)  == typeof(Product))
            {
                return withTracking ?
                (IEnumerable<TEntity>)await _storeContext.Set<Product>()
                .Include(P => P.Brand)
                .Include(P => P.Category)
                .ToListAsync() 
                :
                (IEnumerable<TEntity>)await _storeContext.Set<Product>()
                .Include(P => P.Brand)
                .Include(P => P.Category)
                .AsNoTracking()
                .ToListAsync();
            }
            
            return withTracking?
                await _storeContext.Set<TEntity>().ToListAsync() :
                await _storeContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }


        public async Task<TEntity?> GetAsync(TKey id) => await _storeContext.Set<TEntity>().FindAsync(id);



        public async Task AddAsync(TEntity entity)
        {
            await _storeContext.Set<TEntity>().AddAsync(entity);
        }


        public void Update(TEntity entity)
        {
            _storeContext.Set<TEntity>().Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _storeContext.Set<TEntity>().Remove(entity);
        }


    }
}
