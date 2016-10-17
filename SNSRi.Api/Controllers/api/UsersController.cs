using SNSRi.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using SNSRi.Api.Models;

namespace SNSRi.Web.Controllers.api
{
    public class UsersController : ApiController
    {
        [HttpGet]
        [Route("api/Users")]
        public IHttpActionResult GetUsers()
        {
            var query = new UserQuery();

            return Ok(query.Search());
        }

        [HttpGet]
        [Route("api/Users/Id")]
        public IHttpActionResult GetUserById(int Id)
        {
            var query = new UserQuery();

            return Ok(query.GetById(Id));
        }

        [HttpPost]
        [Route("api/Account/ValidateUser")]
        public IHttpActionResult ValidateUser(SimpleLoginModel model)
        {
            var query = new UserQuery();

            return Ok(query.ValidateUser(model.Email, model.Password));
        }
    }
}
