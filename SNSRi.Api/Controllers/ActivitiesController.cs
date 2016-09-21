using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNSRi.Repository.Query;

namespace SNSRi.Api.Controllers
{
    public class ActivitiesController : ApiController
    {
		[HttpGet]
		[Route("api/Tickets/{ticketId}/Activities")]
		public IHttpActionResult GetActivitiesForTicket(int ticketId)
		{
			var qry = new ActivityQuery();

			return Ok(qry.GetByTicketId(ticketId));
		}

		[HttpGet]
		[Route("api/Activities")]
		public IHttpActionResult GetAllActivities()
		{
			var qry = new ActivityQuery();

			return Ok(qry.Search());
		}

		[HttpGet]
		[Route("api/Activities/{id}")]
		public IHttpActionResult GetActivity(int id)
		{
			var qry = new ActivityQuery();

			return Ok(qry.GetById(id));
		}
	}
}
