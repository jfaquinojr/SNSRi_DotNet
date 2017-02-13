using SNSRi.Entities;
using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Repository
{
    public interface IHomeSeerUnitOfWork: IUnitOfWork
    {
        void FactoryReset(IEnumerable<HSDevice> devices);
        void FactorySync(IEnumerable<HSDevice> devices);

    }
}