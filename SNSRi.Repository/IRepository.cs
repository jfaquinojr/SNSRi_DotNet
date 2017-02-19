using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SNSRi.Repository
{
    public interface IRepository<TEntity> where TEntity: class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll(int pageIndex=1, int rowsCount=10);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int rowsCount = 10);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(int id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}