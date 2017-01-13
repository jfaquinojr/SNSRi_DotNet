using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Entities.HomeSeer
{
    public class HSDevice
    {
        public string Name { get; set; }
        public int Ref { get; set; }
        public string Location { get; set; }
        public string Location2 { get; set; }
        public List<ControlPair> ControlPairs { get; set; }

        public string Value { get; set; }
        public string Status { get; set; }
        public bool HideFromView { get; set; }


    }
}