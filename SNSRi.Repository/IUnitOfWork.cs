using System;

namespace SNSRi.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ITicketRepository Tickets { get; }
        IUserRepository Users { get; }
        IRoomDeviceRepository RoomDevices { get; }
        int Complete();
    }
}