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
    public class UsersController : CustomODataController
    {
        // GET: odata/Users
        public IHttpActionResult GetUsers(ODataQueryOptions<User> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var userQuery = new UserQuery(_page, _perPage);
			var users = userQuery.Search(queryOptions.WhereClause(), queryOptions.OrderByClause());
			return Ok<IEnumerable<User>>(users);
        }

        // GET: odata/Users(5)
        public IHttpActionResult GetUser([FromODataUri] int key, ODataQueryOptions<User> queryOptions)
        {
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

			var userQuery = new UserQuery();
			var user = userQuery.GetById(key);
			return Ok<User>(user);
		}
    }
}
