using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public interface IFactoryResetter
    {
        void FactoryReset();
        void FactorySync();
        ComparisonResult CompareDevices(IEnumerable<HSDevice> devices1, IEnumerable<HSDevice> devices2);
        IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer);
    }
}