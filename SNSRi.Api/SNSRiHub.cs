using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Web
{
    [HubName("snsri")]
    public class SNSRiHub: Hub
    {
        public void TransmitEvent(HSEventMessage eventMsg)
        {
            Clients.All.transmitEvent();
        }

        public class HSEventMessage
        {
            public int EventType { get; set; }
            public IEnumerable<string> Parameters { get; set; }
        }

    }
}