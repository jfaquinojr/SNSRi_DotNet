using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Serilog;
using Serilog.Core;
using SNSRi.Business;
using SNSRi.Entities;
using SNSRi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Web
{
    [HubName("snsri")]
    public class SNSRiHub: Hub
    {
        private IEventMonitor _monitor;
        private ITicketingUnitOfWork _uof;
        public SNSRiHub(IEventMonitor monitor, ITicketingUnitOfWork uof)
        {
            _monitor = monitor;
            _uof = uof;
        }

        public void TransmitEvent(HSEventValueChanged eventMsg)
        {
            Log.Information("Event received {@EventMessage}");

            int ticketId;
            Ticket ticket;
            if(eventMsg.HSEventType == 1024)
            {
                switch (_monitor.CheckEvent(eventMsg))
                {
                    case EventServerity.Emergency:
                        Log.Debug("Emergency event: {@EventMessage}");
                        ticketId = _uof.CreateTicket("Emergency " + eventMsg.ReferenceId + " has occurred", "Emergency", eventMsg.ReferenceId);
                        ticket = _uof.Tickets.Find(t => t.Id == ticketId).FirstOrDefault();
                        Clients.All.transmitEmergency(ticket);
                        break;
                    case EventServerity.Alert:
                        ticketId = _uof.CreateTicket("Alert " + eventMsg.ReferenceId + " has occurred", "Alert", eventMsg.ReferenceId);
                        ticket = _uof.Tickets.Find(t => t.Id == ticketId).FirstOrDefault();
                        Clients.All.transmitAlert();
                        break;
                    case EventServerity.Warning:
                        ticketId = _uof.CreateTicket("Warning " + eventMsg.ReferenceId + " has occurred", "Warning", eventMsg.ReferenceId);
                        ticket = _uof.Tickets.Find(t => t.Id == ticketId).FirstOrDefault();
                        Clients.All.transmitWarning();
                        break;
                    default:
                        break;
                }

                Clients.All.changeEvent(new
                {
                    ReferenceId = eventMsg.ReferenceId,
                    NewValue = eventMsg.NewValue,
                    OldValue = eventMsg.OldValue
                });
            }
        }
    }
}