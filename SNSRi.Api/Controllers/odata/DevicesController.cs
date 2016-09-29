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

namespace SNSRi.odata.Controllers
{
    public class DevicesController2 : CustomODataController
    {

		// GET: odata/Devices
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

			var deviceQuery = new DeviceQuery(_page, _perPage);
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
    }
}
