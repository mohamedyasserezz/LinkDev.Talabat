﻿using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contract.Persistance;
using LinkDev.Talabat.Infrastructure.Persistence.Generic_Repository;
using System.Collections.Concurrent;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeContext;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext storeContext)
        {
            _storeContext = storeContext;

            _repositories = new ConcurrentDictionary<string, object>();
        }


        public async Task<int> CompleteAsync() => await _storeContext.SaveChangesAsync();


        public ValueTask DisposeAsync() => _storeContext.DisposeAsync();

        public IGenricRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            return (IGenricRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_storeContext));
        }
    }
}