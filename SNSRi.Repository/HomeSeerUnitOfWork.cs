using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository.Commands;

namespace SNSRi.Repository
{
    public class HomeSeerUnitOfWork : UnitOfWork, IHomeSeerUnitOfWork
    {

        public HomeSeerUnitOfWork(
            SNSRiContext context,
            ITicketRepository ticketRepository,
            IUserRepository userRepository,
            IDeviceRepository deviceRepository,
            IRoomDeviceRepository roomDeviceRepository,
            IRoomRepository roomRepository,
            IHSDeviceRepository hsDeviceRepository) : base(context, ticketRepository, userRepository, deviceRepository, roomDeviceRepository, roomRepository, hsDeviceRepository)
        {
        }

        public void FactoryReset(IEnumerable<HSDevice> hsDevices, Func<HSDevice, Device> deviceConverter, Func<string, UIRoom> roomConverter)
        {

            using (var dbTran = base._context.Database.BeginTransaction())
            {
                try
                {
                    Truncate("UIRoomDevice");
                    Truncate("UIRoom");
                    Truncate("Device");
                    Truncate("HSDevice");

                    var locations = hsDevices.Select(d => d.Location);
                    foreach (string location in locations)
                    {
                        this.Rooms.Add(roomConverter(location));
                    }

                    foreach (var hsDevice in hsDevices)
                    {
                        var device = deviceConverter(hsDevice);
                        this.Devices.Add(device);
                        _context.SaveChanges(); // get a fresh DeviceID from DB


                        var rd = new UIRoomDevice
                        {
                            UIRoomId = this.Rooms.GetAll().First(r => r.SourceRoom == hsDevice.Location).Id,
                            DeviceId = device.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now
                        };
                        this.RoomDevices.Add(rd);

                        this.HSDevices.Add(hsDevice);
                    }

                    _context.SaveChanges();
                    dbTran.Commit();
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    throw;
                }

            }
        }

        public void FactorySync(IEnumerable<HSDevice> devices)
        {
            throw new NotImplementedException();
        }

        private void Truncate(string table)
        {
            base._context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }


        //public void AddUIDevices(IEnumerable<Device> devices, string roomName)
        //{
        //    foreach (var device in devices)
        //    {
        //        _context.Devices.Add(device);
        //        _context.SaveChanges();
        //        AddUIRoomDevice(device, roomName);
        //    }
            
        //}

        //private void AddUIRoomDevice(Device dev, string roomName)
        //{
        //    var room = _context.Rooms.FirstOrDefault(r => r.SourceRoom == roomName || r.Name == roomName);
        //    if (room != null)
        //    {
        //        _context.RoomDevices.Add(new UIRoomDevice()
        //        {
        //            DeviceId = dev.Id,
        //            UIRoomId = room.Id
        //        });
        //    }
        //    else
        //    {
        //        _context.RoomDevices.Add(new UIRoomDevice()
        //        {
        //            DeviceId = dev.Id,
        //            UIRoomId = _context.Rooms.First().Id
        //        });
        //    }
        //}

        //private void DeleteHSDevices(IEnumerable<HSDevice> devices)
        //{
        //    foreach (var device in devices)
        //    {

        //        DeleteUIRoomDevice(device);

        //        var devToRemove = _context.HSDevices.Single(d => d.Name == device.Name && d.Location == device.Location && d.Location2 == device.Location2 && d.Ref == device.Ref);
        //        _context.HSDevices.Remove(devToRemove);
        //    }
        //}

        //private void DeleteUIRoomDevice(HSDevice device)
        //{
        //    var dev = _context.RoomDevices.FirstOrDefault(r => r.DeviceId == device.Id);
        //    if (dev != null)
        //    {
        //        _context.RoomDevices.Remove(dev);
        //    }
        //}
    }
}