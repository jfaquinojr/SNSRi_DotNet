using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;
using static SNSRi.Repository.ConnectionFactory;

namespace SNSRi.Repository
{
    public class SNSRiContext : DbContext
    {
        public SNSRiContext() : base((DbConnection) CreateSQLiteConnection(), true)
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventTicket> EventTickets { get; set; }
        public DbSet<UIRoom> Rooms { get; set; }
        public DbSet<UIRoomDevice> RoomDevices { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<HSDevice> HSDevices { get; set; }
    }
}