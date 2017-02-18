using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public static class ObjectConverter
    {
        public static Device ConvertToDevice(HSDevice hsDevice)
        {
            var device = new Device
            {
                Name = hsDevice.Name,
                ReferenceId = hsDevice.Ref,
                Status = hsDevice.Status,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                Value = hsDevice.Value,
                HideFromView = hsDevice.HideFromView.GetValueOrDefault()
            };


            var hideDevicesWithTheseWords = new string[]
            {
                "battery",
                "root"
            };

            if (!string.IsNullOrEmpty(device.Name) && hideDevicesWithTheseWords.Any(device.Name.ToLower().Contains))
            {
                device.HideFromView = true;
            }

            return device;
        }

        public static UIRoom ConvertToRoom(string roomName)
        {
            if (string.IsNullOrWhiteSpace(roomName))
            {
                throw new InvalidOperationException("Room name must not be empty");
            }

            if (roomName.ToLower() == "all")
            {
                throw new InvalidOperationException("All room should be skipped");
            }

            return new UIRoom
            {
                Name = roomName,
                SourceRoom = roomName,
                CreatedOn = DateTime.Now
            };
        }
    }
}
