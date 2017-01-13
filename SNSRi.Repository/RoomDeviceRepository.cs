using System.Data.Entity;
using SNSRi.Entities;

namespace SNSRi.Repository
{
    public class RoomDeviceRepository: Repository<UIRoomDevice>, IRoomDeviceRepository
    {
        public RoomDeviceRepository(SNSRiContext context) : base(context)
        {
        }
    }
}