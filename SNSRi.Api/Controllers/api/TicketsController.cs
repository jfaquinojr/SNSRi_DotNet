using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SNSRi.Entities;
using SNSRi.Repository.Query;

namespace SNSRi.Api.Controllers
{
    public class TicketsController : ApiController
    {

		[HttpGet]
		[Route("api/Tickets/Open")]
		public IHttpActionResult GetOpenTickets()
		{
			var qryDevice = new TicketQuery();


			return Ok(qryDevice.GetOpenEventTickets());
		}

		[HttpGet]
		[Route("api/Tickets/{id}")]
		public IHttpActionResult GetTicket(int id)
		{
			var qryDevice = new TicketQuery();


			return Ok(qryDevice.GetById(id));
		}


		[HttpGet]
		[Route("api/Tickets/Open/Room/{Id}")]
		public IHttpActionResult GetOpenTicketsByRoom(int Id)
		{
			var qryDevice = new TicketQuery();


			return Ok(qryDevice.GetOpenEventTicketsByRoom(Id));
		}

		[HttpGet]
		[Route("api/Tickets/Open/Past/Minutes/{minutes}")]
		public IHttpActionResult GetOpenTicketsPastMinutes(int minutes = 1)
		{
			var qryDevice = new TicketQuery();


			return Ok(qryDevice.GetOpenTicketsPastMinutes(minutes));
		}


	}
}
