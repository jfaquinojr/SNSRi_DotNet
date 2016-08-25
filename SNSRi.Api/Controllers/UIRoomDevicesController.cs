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
    public class UIRoomDevicesController : CustomODataController
    {
        // GET: odata/UIRoomDevices
        public IHttpActionResult GetUIRoomDevices(ODataQueryOptions<UIRoomDevice> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var query = new UIRoomDeviceQuery(_page, _perPage);
			var result = query.Search(queryOptions.WhereClause(), queryOptions.OrderByClause());
			return Ok<IEnumerable<UIRoomDevice>>(result);
		}

        // GET: odata/UIRoomDevices(5)
        public IHttpActionResult GetUIRoomDevice([FromODataUri] int key, ODataQueryOptions<UIRoomDevice> queryOptions)
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

			var query = new UIRoomDeviceQuery();
			var result = query.GetById(key);
			return Ok<UIRoomDevice>(result);
		}

        // PUT: odata/UIRoomDevices(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<UIRoomDevice> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(uIRoomDevice);

            // TODO: Save the patched entity.

            // return Updated(uIRoomDevice);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/UIRoomDevices
        public IHttpActionResult Post(UIRoomDevice uIRoomDevice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(uIRoomDevice);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/UIRoomDevices(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<UIRoomDevice> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(uIRoomDevice);

            // TODO: Save the patched entity.

            // return Updated(uIRoomDevice);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/UIRoomDevices(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
