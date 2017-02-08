using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Business
{
    public class FactoryReset
    {
        private static FactoryReset _factoryReset;

        private FactoryReset()
        {
        }

        public static FactoryReset Instance => _factoryReset ?? (_factoryReset = new FactoryReset());

        public ComparisonResult CompareDevices(IEnumerable<HSDevice> devices1, IEnumerable<HSDevice> devices2)
        {
            var result = new ComparisonResult();

            var deletedDevices = devices1.Except(devices2).ToList(); 
            var addedDevices = devices2.Except(devices1).ToList(); 

            result.AddedDevices.AddRange(addedDevices);
            result.DeletedDevices.AddRange(deletedDevices);

            return result;
        }

    }
}
