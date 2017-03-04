using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SNSRi.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Web
{
    [HubName("snsri")]
    public class SNSRiHub: Hub
    {
        public void TransmitEvent(HSEventValueChanged eventMsg)
        {
            var monitor = new EventMonitor();
            if(eventMsg.HSEventType == 1024)
            {
                switch (monitor.CheckEvent(eventMsg))
                {
                    case EventServerity.Emergency:
                        Clients.All.transmitEmergency();
                        break;
                    default:
                        Clients.All.changeEvent(eventMsg.ReferenceId, eventMsg.NewValue, eventMsg.OldValue);
                        break;
                }
            }
        }
    }
}