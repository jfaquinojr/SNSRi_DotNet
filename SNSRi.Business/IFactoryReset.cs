using System.Collections.Generic;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public interface IFactoryReset
    {
        ComparisonResult CompareDevices(IEnumerable<HSDevice> devices1, IEnumerable<HSDevice> devices2);
        IEnumerable<HSDevice> GetHSDevices(string urlHomeSeer);
    }
}