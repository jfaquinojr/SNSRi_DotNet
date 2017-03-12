using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository.Commands;

namespace SNSRi.Repository
{
    public class TicketingUnitOfWork : UnitOfWork, ITicketingUnitOfWork
    {

        public TicketingUnitOfWork(
            SNSRiContext context,
            ITicketRepository ticketRepository,
            IUserRepository userRepository,
            IDeviceRepository deviceRepository,
            IRoomDeviceRepository roomDeviceRepository,
            IRoomRepository roomRepository,
            IResidentRepository residentRepository,
            IHSDeviceRepository hsDeviceRepository,
            IDeviceControlRepository deviceControlRepository,
            IEventRepository eventRepository) : base(context, ticketRepository, userRepository, deviceRepository, roomDeviceRepository, roomRepository, hsDeviceRepository, residentRepository, deviceControlRepository, eventRepository)
        {
        }

        public int CreateTicket(string name, string type, int eventId)
        {
            var ticket = new Ticket
            {
                Name = name,
                Status = "New",
                TicketType = type,
                CreatedBy = SessionProvider.CurrentUserId,
                CreatedOn = DateTime.Now
            };
            this.Tickets.Add(ticket);
            _context.SaveChanges();

            var eventTicket = new EventTicket();
            eventTicket.EventId = eventId;
            eventTicket.TicketId = ticket.Id;
            _context.EventTickets.Add(eventTicket);
            _context.SaveChanges();

            return ticket.Id;
        }
    }
}