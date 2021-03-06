﻿using System;
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
            IResidentRepository residentRepository,
            IHSDeviceRepository hsDeviceRepository,
            IDeviceControlRepository deviceControlRepository,
            IEventRepository eventRepository) : base(context, ticketRepository, userRepository, deviceRepository, roomDeviceRepository, roomRepository, hsDeviceRepository, residentRepository, deviceControlRepository, eventRepository)
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
                    Truncate("DeviceControl");

                    var roomNames = hsDevices.Select(d => d.Location2).Distinct();
                    foreach (var roomName in roomNames)
                    {
                        this.Rooms.Add(roomConverter(roomName));
                    }

                    foreach (var hsDevice in hsDevices)
                    {
                        var device = deviceConverter(hsDevice);
                        this.Devices.Add(device);
                        _context.SaveChanges(); // get a fresh DeviceID from DB


                        var rd = new UIRoomDevice
                        {
                            UIRoomId = this.Rooms.GetAll().First(r => r.SourceRoom == hsDevice.Location2).Id,
                            DeviceId = device.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now
                        };
                        this.RoomDevices.Add(rd);

                        this.HSDevices.Add(hsDevice);


                        foreach (var pair in hsDevice.ControlPairs)
                        {
                            this.DeviceControls.Add(new DeviceControl
                            {
                                DoUpdate = pair.Do_Update,
                                SingleRangeEntry = pair.SingleRangeEntry,
                                ButtonType = pair.ControlButtonType,
                                ButtonCustom = pair.ControlButtonCustom,
                                CCIndex = pair.CCIndex,
                                Range = pair.Range,
                                DeviceId = device.Id,
                                Label = pair.Label,
                                ControlType = pair.ControlType,
                                ControlValue = pair.ControlValue,
                                ControlString = pair.ControlString,
                                ControlStringList = pair.ControlStringList,
                                ControlStringSelected = pair.ControlStringSelected,
                                ControlFlag = pair.ControlFlag
                            });
                        }
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

        public void FactorySync(IEnumerable<HSDevice> addedDevices, IEnumerable<HSDevice> deletedDevices, Func<HSDevice, Device> deviceConverter)
        {
            using (var dbTran = base._context.Database.BeginTransaction())
            {
                try
                {
                    DeleteHSDevices(deletedDevices);
                    AddHSDevices(addedDevices, deviceConverter);
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

        private void AddHSDevices(IEnumerable<HSDevice> devices, Func<HSDevice, Device> deviceConverter)
        {
            foreach (var dev in devices)
            {
                _context.HSDevices.Add(dev);
                _context.SaveChanges();

                AddUIDevice(deviceConverter(dev), dev.Location);
            }
        }

        public void AddUIDevice(Device device, string roomName)
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            AddUIRoomDevice(device, roomName);
        }

        private void AddUIRoomDevice(Device dev, string roomName)
        {
            var room = _context.Rooms.FirstOrDefault(r => r.SourceRoom == roomName || r.Name == roomName);
            if (room != null)
            {
                _context.RoomDevices.Add(new UIRoomDevice()
                {
                    DeviceId = dev.Id,
                    UIRoomId = room.Id
                });
            }
            else
            {
                _context.RoomDevices.Add(new UIRoomDevice()
                {
                    DeviceId = dev.Id,
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

        private void Truncate(string table)
        {
            base._context.Database.ExecuteSqlCommand($"DELETE FROM {table}");
        }
    }
}