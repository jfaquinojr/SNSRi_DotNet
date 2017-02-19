using System.Collections.Generic;
using System.Data.Entity;
using SNSRi.Entities;
using System.Linq;

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

        private SNSRiContext SNSRiContext
        {
            get
            {
                return Context as SNSRiContext;
            }
        }


    }
}