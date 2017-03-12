namespace SNSRi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITicketRepository Tickets { get; set; }
        public IUserRepository Users { get; set; }
        public IDeviceRepository Devices { get; set; }
        public IRoomDeviceRepository RoomDevices { get; set; }
        public IRoomRepository Rooms { get; set; }
        public IResidentRepository Residents { get; set; }
        public IHSDeviceRepository HSDevices { get; set; }
        public IDeviceControlRepository DeviceControls { get; set; }
        public IEventRepository Events { get; set; }

        protected readonly SNSRiContext _context;

        public UnitOfWork(SNSRiContext context, 
            ITicketRepository ticketRepository,
            IUserRepository userRepository, 
            IDeviceRepository deviceRepository,
            IRoomDeviceRepository roomDeviceRepository,
            IRoomRepository roomRepository, 
            IHSDeviceRepository hsDeviceRepository,
            IResidentRepository residentRepository,
            IDeviceControlRepository deviceControlRepository,
            IEventRepository eventRepository)
        {
            _context = context;
            Tickets = ticketRepository;
            Users = userRepository;
            Devices = deviceRepository;
            RoomDevices = roomDeviceRepository;
            Rooms = roomRepository;
            HSDevices = hsDeviceRepository;
            Residents = residentRepository;
            DeviceControls = deviceControlRepository;
            Events = eventRepository;
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