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
        public void TransmitEvent(HSEventMessage eventMsg)
        {
            if(eventMsg.HSEventType == 1024)
            {
                HSEventValueChanged changedEvent = eventMsg as HSEventValueChanged;
                Clients.All.transmitEvent();
            }
        }
    }
}