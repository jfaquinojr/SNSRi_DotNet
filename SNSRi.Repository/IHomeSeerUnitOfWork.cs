using SNSRi.Entities;
using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Repository
{
    public interface IHomeSeerUnitOfWork: IUnitOfWork
    {
        void FactoryReset(string urlHomeSeer);
        IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer);
        HSLocation GetHSLocation(string urlHomeSeer);
    }
}