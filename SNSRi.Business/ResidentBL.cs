using System;
using System.Collections.Generic;
using SNSRi.Entities;
using SNSRi.Repository;

namespace SNSRi.Business
{
    public class ResidentBL : IResidentBL
    {
        IUnitOfWork _uof;
        public ResidentBL(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public int Create(Resident entity)
        {
            _uof.Residents.Add(entity);
            _uof.Complete();
            return entity.Id;
        }

        public void Delete(int Id)
        {
            _uof.Residents.Remove(Id);
            _uof.Complete();
        }

        public Resident Get(int Id)
        {
            return _uof.Residents.Get(Id);
        }

        public IEnumerable<Resident> GetAll()
        {
            return _uof.Residents.GetAll();
        }

        public void Update(Resident entity)
        {
            _uof.Residents.Update(entity);
            _uof.Complete();
        }
    }
}
