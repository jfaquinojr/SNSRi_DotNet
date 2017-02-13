using System;

namespace SNSRi.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITicketRepository Tickets { get; set; }
        IUserRepository Users { get; set; }
        IDeviceRepository Devices { get; set; }
        IRoomDeviceRepository RoomDevices { get; set; }
        IRoomRepository Rooms { get; set; }
        IHSDeviceRepository HSDevices { get; set; }
        int Complete();
    }
}