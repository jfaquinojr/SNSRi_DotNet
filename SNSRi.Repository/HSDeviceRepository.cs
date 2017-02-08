using System.Data.Entity;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Repository
{
    public class HSDeviceRepository : Repository<HSDevice>, IHSDeviceRepository
    {
        public HSDeviceRepository(DbContext context) : base(context)
        {
        }
    }
}