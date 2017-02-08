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

        public static UIRoom ConvertToRoom(string roomName, int userId)
        {
            if (roomName.ToLower() == "all")
            {
                throw new ArgumentException("All room should be skipped", nameof(roomName));
            }

            return new UIRoom
            {
                Name = roomName,
                CreatedOn = DateTime.Now,
                CreatedBy = userId
            };
        }
    }
}
