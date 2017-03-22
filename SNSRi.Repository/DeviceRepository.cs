using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(SNSRiContext context) : base(context)
        {
        }

        public Device GetByReferenceId(int referenceId)
        {
            return SNSRiContext.Devices
                .Include(i => i.DeviceControls)
                .FirstOrDefault(d => d.ReferenceId == referenceId);
        }

        public IEnumerable<Device> GetByRoomId(int roomId)
        {
            return SNSRiContext.RoomDevices
                .Where(rd => rd.UIRoomId == roomId)
                .Select(rd => rd.Device)
                .ToList();
        }

        public SNSRiContext SNSRiContext => base.Context as SNSRiContext;
    }
}