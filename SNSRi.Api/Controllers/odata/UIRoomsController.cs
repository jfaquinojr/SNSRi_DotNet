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
    public class UIRoomsController : CustomODataController
    {
        // GET: odata/UIRooms
        public IHttpActionResult GetUIRooms(ODataQueryOptions<UIRoom> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var roomQuery = new UIRoomQuery(_page, _perPage);
			var rooms = roomQuery.Search(queryOptions.WhereClause(), queryOptions.OrderByClause());
			return Ok<IEnumerable<UIRoom>>(rooms);
		}

        // GET: odata/UIRooms(5)
        public IHttpActionResult GetUIRoom([FromODataUri] int key, ODataQueryOptions<UIRoom> queryOptions)
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

			var roomQuery = new UIRoomQuery();
			var room = roomQuery.GetById(key);
			return Ok<UIRoom>(room);
		}
    }
}
