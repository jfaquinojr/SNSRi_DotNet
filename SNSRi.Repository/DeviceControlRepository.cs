using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public class DeviceControlRepository : Repository<DeviceControl>, IDeviceControlRepository
    {
        public DeviceControlRepository(SNSRiContext context) : base(context)
        {
        }

        public SNSRiContext SNSRiContext => base.Context as SNSRiContext;
    }
}