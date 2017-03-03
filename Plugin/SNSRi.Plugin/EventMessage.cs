using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static HomeSeerAPI.Enums;

namespace SNSRi.Plugin
{
    public class EventMessage
    {
        public int HSEventType { get; set; }
        public IEnumerable<string> Parameters { get; set; }
    }
}
