using System.Collections;
using System.Collections.Generic;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public interface IDeviceRepository: IRepository<Device>
    {
        Device GetByReferenceId(int referenceId);
        IEnumerable<Device> GetByRoomId(int roomId);
    }
}