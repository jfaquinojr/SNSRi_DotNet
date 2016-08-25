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
    public class DevicesController : CustomODataController
    {

		// GET: odata/Devices
		[EnableQuery()]
		public IHttpActionResult GetDevices(ODataQueryOptions<Device> queryOptions)
        {
			try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var page = this.Request.RequestUri.GetQueryIntegerValue("page");
			var perPage = this.Request.RequestUri.GetQueryIntegerValue("per_page");

			var deviceQuery = new DeviceQuery(page, perPage);
			var devices = deviceQuery.Search(queryOptions.WhereClause(), queryOptions.OrderByClause());
            return Ok<IEnumerable<Device>>(devices);
        }

        // GET: odata/Devices(5)
        public IHttpActionResult GetDevice([FromODataUri] int key, ODataQueryOptions<Device> queryOptions)
        {
			try
			{
				queryOptions.Validate(_validationSettings);
			}
			catch (ODataException ex)
			{
				return BadRequest(ex.Message);
			}

			var deviceQuery = new DeviceQuery();
			var device = deviceQuery.GetById(key);
			return Ok<Device>(device);
		}

        // PUT: odata/Devices(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Device> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(device);

            // TODO: Save the patched entity.

            // return Updated(device);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Devices
        public IHttpActionResult Post(Device device)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(device);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Devices(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Device> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(device);

            // TODO: Save the patched entity.

            // return Updated(device);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Devices(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
