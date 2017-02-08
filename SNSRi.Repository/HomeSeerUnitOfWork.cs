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
                    
                    throw;
                }
                
            }


            
        }

        public void FactorySync(IEnumerable<HSDevice> devices)
        {
            throw new NotImplementedException();
        }
    }
}