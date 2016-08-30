using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using System.Web.Http.OData.Routing;
using SNSRi.Entities;
using Microsoft.Data.OData;
using SNSRi.Repository.Query;
using System.Web.Http.Cors;

namespace SNSRi.odata.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EventsController : CustomODataController
    {
        // GET: odata/Events
        public IHttpActionResult GetEvents(ODataQueryOptions<Event> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var eventQuery = new EventQuery(_page, _perPage);
			var events = eventQuery.Search(queryOptions.WhereClause(), queryOptions.OrderByClause());
			return Ok<IEnumerable<Event>>(events);
		}

        // GET: odata/Events(5)
        public IHttpActionResult GetEvent([FromODataUri] int key, ODataQueryOptions<Event> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var eventQuery = new EventQuery();
			var theEvent = eventQuery.GetById(key);
			return Ok<Event>(theEvent);
		}
    }
}
