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

        // PUT: odata/Users(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<User> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(user);

            // TODO: Save the patched entity.

            // return Updated(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Users
        public IHttpActionResult Post(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Users(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<User> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(user);

            // TODO: Save the patched entity.

            // return Updated(user);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Users(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
