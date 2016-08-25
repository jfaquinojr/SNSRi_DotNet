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
using SNSRi.Repository;

namespace SNSRi.Api.Controllers
{
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

        // PUT: odata/Events(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Event> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(@event);

            // TODO: Save the patched entity.

            // return Updated(@event);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Events
        public IHttpActionResult Post(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(@event);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Events(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Event> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(@event);

            // TODO: Save the patched entity.

            // return Updated(@event);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Events(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
