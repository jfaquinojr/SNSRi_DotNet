using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Entities.HomeSeer
{
    public class HSLocations
    {
        public List<string> Floors { get; set; } = new List<string>();
        public List<string> Rooms { get; set; } = new List<string>();
    }

    public class HSLocation
    {
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public List<HSDevice> HSDevices { get; set; }
    }
}