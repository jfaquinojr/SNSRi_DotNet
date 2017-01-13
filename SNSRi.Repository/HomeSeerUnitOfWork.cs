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
        private void Truncate(string table)
        {
            base._context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }

        public HomeSeerUnitOfWork(SNSRiContext context) : base(context)
        {
            
        }

        public void FactoryReset(string urlHomeSeer)
        {
            var hsDevices = GetHSDevices(urlHomeSeer);
            var hsLocation = GetHSLocation(urlHomeSeer);

            using (var dbTran = base._context.Database.BeginTransaction())
            {
                try
                {
                    Truncate("UIRoomDevice");
                    Truncate("UIRoom");
                    Truncate("Device");

                    //var cmdRoom = new UIRoomCommand();
                    var rooms = new List<UIRoom>();
                    foreach (var hsLocationRoom in hsLocation.Rooms)
                    {
                        if (hsLocationRoom.ToLower() == "all") // skip this room
                            continue;

                        var room = new UIRoom
                        {
                            Name = hsLocationRoom,
                            CreatedOn = DateTime.Now,
                            CreatedBy = 1
                        };
                        this.Rooms.Add(room);
                        rooms.Add(room);
                    }


                    foreach (var hsDevice in hsDevices)
                    {
                        var device = new Device
                        {
                            Name = hsDevice.Name,
                            ReferenceId = hsDevice.Ref,
                            Status = hsDevice.Status,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                            Value = hsDevice.Value,
                            HideFromView = hsDevice.HideFromView
                        };
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

        public IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer)
        {
            var ret = new List<HSDevice>();
            urlHomeSeer = urlHomeSeer.TrimEnd('/') + "/JSON?request=getstatus";
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(urlHomeSeer);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Result);

                var hsDevices = results.Devices;
                foreach (var hsDev in hsDevices)
                {
                    ret.Add(new HSDevice
                    {
                        Name = hsDev.name,
                        Status = hsDev.status,
                        Location = hsDev.location,
                        Ref = hsDev.@ref,
                        Value = hsDev.value.ToString(),
                        HideFromView = (bool)hsDev.hide_from_view,
                        Location2 = hsDev.location2
                    });
                }
            }
            return ret;
        }

        public HSLocation GetHSLocation(string urlHomeSeer)

        {
            var ret = new HSLocation();
            urlHomeSeer = urlHomeSeer.TrimEnd('/') + "/JSON?request=getlocations";
            using (var client = new HttpClient())
            {
                var response = client.GetStringAsync(urlHomeSeer);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Result);

                var rooms = results.location1;
                foreach (var hsRoom in rooms)
                {
                    ret.Rooms.Add(hsRoom.ToString());
                }

                var floors = results.location2;
                foreach (var hsFloor in floors)
                {
                    ret.Floors.Add(hsFloor.ToString());
                }
            }
            return ret;
        }
    }
}