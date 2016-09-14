using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using SNSRi.Repository.Query;

namespace SNSRi.Web.Controllers
{
    public class ActionsController : ApiController
    {
		// GET: api/Actions
        [HttpGet()]
		[Route("api/Actions/GetOpenTickets/")]
		public IHttpActionResult GetOpenTickets()
		{
		    var query = new TicketQuery();
		    var result = query.GetOpenEventTickets();

	        return Ok(result);
        }

		// GET: api/Actions/5
		[Route("api/Actions/{id}")]
		public IHttpActionResult Get(int id)
        {
            return Ok("value");
        }

        // POST: api/Actions
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Actions/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Actions/5
        public void Delete(int id)
        {
        }
    }
}
