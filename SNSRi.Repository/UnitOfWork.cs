namespace SNSRi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITicketRepository Tickets { get; private set; }
        public IUserRepository Users { get; private set; }
        public IDeviceRepository Devices { get; set; }
        public IRoomDeviceRepository RoomDevices { get; set; }
        public IRoomRepository Rooms { get; set; }
        public IHSDeviceRepository HSDevices { get; set; }

        protected readonly SNSRiContext _context;

        public UnitOfWork(SNSRiContext context)
        {
            _context = context;
            Tickets = new TicketRepository(_context);
            Users = new UserRepository(_context);
            Devices = new DeviceRepository(_context);
            RoomDevices = new RoomDeviceRepository(_context);
            Rooms = new RoomRepository(_context);
            HSDevices = new HSDeviceRepository(_context);
        }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}