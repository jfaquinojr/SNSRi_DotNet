using System.Collections;
using System.Collections.Generic;

namespace SNSRi.Business
{
    public interface IBusinessDataLayer<TEntity> : IBusinessLayer
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(int Id);
        void Update(TEntity entity);
        int Create(TEntity entity);
        void Delete(int Id);
    }
}