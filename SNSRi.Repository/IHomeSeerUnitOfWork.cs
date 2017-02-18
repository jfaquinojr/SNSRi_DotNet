using SNSRi.Entities;
using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;
using System;

namespace SNSRi.Repository
{
    public interface IHomeSeerUnitOfWork: IUnitOfWork
    {
        void FactoryReset(IEnumerable<HSDevice> hsDevices, Func<HSDevice, Device> deviceConverter, Func<string, UIRoom> roomConverter);
        void FactorySync(IEnumerable<HSDevice> hsDevices);
        //void AddUIDevices(IEnumerable<Device> devices, string roomName);
    }
}