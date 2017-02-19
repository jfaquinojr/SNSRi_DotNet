using System.Collections.Generic;
using System.Data.Entity;
using SNSRi.Entities;
using System.Linq;
using System;

namespace SNSRi.Repository
{

    public class ResidentRepository: Repository<Resident>, IResidentRepository
    {
        public ResidentRepository(DbContext context) : base(context)
        {
        }

        public override IEnumerable<Resident> GetAll(int pageIndex, int rowsCount = 10)
        {
            var residents = SNSRiContext.Residents
                .Include(e => e.UIRoom)
                .ToList();
            return residents;
        }

        public IEnumerable<Resident> GetAllByRoomId(int Id)
        {
            return SNSRiContext.Residents.Where(r => r.UIRoomId == Id).ToList();
        }

        private SNSRiContext SNSRiContext
        {
            get
            {
                return Context as SNSRiContext;
            }
        }


    }
}