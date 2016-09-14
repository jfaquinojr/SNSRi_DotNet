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
		[Route("api/Tickets/GetOpenTickets")]
		public IHttpActionResult GetOpenTickets()
		{
			var qryDevice = new TicketQuery();


			return Ok(qryDevice.GetOpenEventTickets());
		}

	}
}
