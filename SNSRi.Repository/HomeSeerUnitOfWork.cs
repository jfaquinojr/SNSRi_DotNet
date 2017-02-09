using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SNSRi.Business;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository.Commands;

namespace SNSRi.Repository
{
    public class HomeSeerUnitOfWork : UnitOfWork, IHomeSeerUnitOfWork
    {
        private void Truncate(string table)
        {
            base._context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }

        public HomeSeerUnitOfWork(SNSRiContext context) : base(context)
        {
            
        }

        public void FactoryReset(IEnumerable<HSDevice> devices)
        {
            var hsDevices = devices;
            var hsLocations = devices.Select(d => d.Location).Distinct();

            using (var dbTran = base._context.Database.BeginTransaction())
            {
                try
                {
                    Truncate("UIRoomDevice");
                    Truncate("UIRoom");
                    Truncate("Device");
                    Truncate("HSDevice");

                    var rooms = new List<UIRoom>();
                    foreach (var hsLocationRoom in hsLocations)
                    {
                        if (hsLocationRoom.ToLower() == "all") // skip this room
                            continue;

                        var room = ObjectConverter.ConvertToRoom(hsLocationRoom, 1);
                        this.Rooms.Add(room);
                        rooms.Add(room);
                    }

                    foreach (var hsDevice in hsDevices)
                    {
                        var device = ObjectConverter.ConvertToDevice(hsDevice);
                        this.Devices.Add(device);
                        _context.SaveChanges(); // get a fresh ID from DB

                       
                        var rd = new UIRoomDevice
                        {
                            UIRoomId = rooms.First(r => r.Name == hsDevice.Location).Id,
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
            var currentDevices = this._context.HSDevices.ToList();
            var result = SNSRi.Business.FactoryReset.Instance.CompareDevices(currentDevices, devices);

            using (var dbTran = base._context.Database.BeginTransaction())
            {
                try
                {
                    DeleteHSDevices(result.DeletedDevices);
                    AddHSDevices(result.AddedDevices);
                    _context.SaveChanges();
                    dbTran.Commit();
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        private void AddHSDevices(IEnumerable<HSDevice> devices)
        {
            foreach (var dev in devices)
            {
                _context.HSDevices.Add(dev);
                _context.SaveChanges();

                AddUIDevice(dev);
            }

        }

        private void AddUIDevice(HSDevice dev)
        {
            var uiDevice = ObjectConverter.ConvertToDevice(dev);
            _context.Devices.Add(uiDevice);

            AddUIRoomDevice(dev, uiDevice);
        }

        private void AddUIRoomDevice(HSDevice hsDev, Device uiDev)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.SourceRoom == hsDev.Location);
            if (room != null)
            {
                _context.RoomDevices.Add(new UIRoomDevice()
                {
                    DeviceId = uiDev.Id,
                    UIRoomId = room.Id
                });
            }
            else
            {
                _context.RoomDevices.Add(new UIRoomDevice()
                {
                    DeviceId = uiDev.Id,
                    UIRoomId = _context.Rooms.First().Id
                });
            }
        }

        private void DeleteHSDevices(IEnumerable<HSDevice> devices)
        {
            foreach (var device in devices)
            {

                DeleteUIRoomDevice(device);

                var devToRemove = _context.HSDevices.Single(d => d.Name == device.Name && d.Location == device.Location && d.Location2 == device.Location2 && d.Ref == device.Ref);
                _context.HSDevices.Remove(devToRemove);
            }
        }

        private void DeleteUIRoomDevice(HSDevice device)
        {
            var dev = _context.RoomDevices.FirstOrDefault(r => r.DeviceId == device.Id);
            if (dev != null)
            {
                _context.RoomDevices.Remove(dev);
            }
        }
    }
}