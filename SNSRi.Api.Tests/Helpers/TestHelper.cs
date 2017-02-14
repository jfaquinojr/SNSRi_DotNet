using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Api.Tests.Helpers
{
    public static class TestHelper
    {
        public static HSDevice CreateHSDevice(string name, string location1, string location2, string value, string status, bool hide, int refId)
        {
            return new HSDevice
            {
                Name = name,
                Location = location1,
                Location2 = location2,
                Value = value,
                Status = status,
                HideFromView = hide,
                Ref = refId
            };
        }
    }
}
