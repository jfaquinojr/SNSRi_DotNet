using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;

namespace SNSRi.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual IEnumerable<TEntity> GetAll(int pageIndex, int rowsCount = 10)
        {
            if (pageIndex < 1)
            {
                throw new ArgumentException("Invalid page number. Must be > 0", nameof(pageIndex));
            }

            if (rowsCount < 1)
            {
                throw new ArgumentException("Invalid rows count. Must be > 0", nameof(rowsCount));
            }

            return Context.Set<TEntity>()
                //.Skip((pageIndex - 1) * rowsCount)
                //.Take(rowsCount)
                .ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, int pageIndex = 1, int rowsCount = 10)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Remove(int id)
        {
            Remove(Get(id));
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public void Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }
    }
}