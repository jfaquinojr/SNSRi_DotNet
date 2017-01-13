using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using SNSRi.Entities;
using System.Data;

namespace SNSRi.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        public TicketRepository(SNSRiContext context) : base(context)
        {
        }

        public IEnumerable<Ticket> GetOpenEventTickets()
        {
            return SNSRiContext.Tickets
                .Where(t => t.TicketType == "Event" && t.Status == "Open")
                .OrderByDescending(t => t.CreatedOn)
                .ToList();
        }

        public IEnumerable<Ticket> GetOpenEventTicketsByRoom(int roomId)
        {
            return SNSRiContext.EventTickets
                .Include(et => et.Ticket)
                .Where(et => et.Ticket.TicketType == "Event" && et.Ticket.Status == "Open")
                .OrderByDescending(et => et.Ticket.CreatedOn)
                .Select(et => et.Ticket)
                .ToList();
        }

        public IEnumerable<Ticket> GetOpenTicketsPast(int ms)
        {
            var pastDateTime = DateTime.Now.AddMilliseconds(-ms);
            return SNSRiContext.Tickets
                .Where(t => t.TicketType == "Event" && t.Status == "Open" && t.CreatedOn >= pastDateTime)
                .OrderByDescending(t => t.CreatedOn)
                .ToList();
        }

        public IEnumerable<Ticket> GetOpenTicketsPastMinutes(int minutes)
        {
            return GetOpenTicketsPast(minutes*60000);
        }

        public SNSRiContext SNSRiContext {
            get
            {
                return base.Context as SNSRiContext;
            }
        }
    }
}