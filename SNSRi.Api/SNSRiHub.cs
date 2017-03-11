using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Serilog;
using Serilog.Core;
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
            Log.Information("Event received {@EventMessage}");
            var monitor = new EventMonitor();
            if(eventMsg.HSEventType == 1024)
            {
                switch (monitor.CheckEvent(eventMsg))
                {
                    case EventServerity.Emergency:
                        Log.Debug("Emergency event: {@EventMessage}");
                        Clients.All.transmitEmergency();
                        break;
                    default:
                        Clients.All.changeEvent(new
                        {
                            ReferenceId = eventMsg.ReferenceId,
                            NewValue    = eventMsg.NewValue,
                            OldValue    = eventMsg.OldValue
                        });
                        break;
                }
            }
        }
    }
}