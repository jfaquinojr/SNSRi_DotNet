using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNSRi.Repository.Query;

namespace SNSRi.Api.Controllers.api
{
    public class RoomsController : ApiController
    {
        [HttpGet]
        [Route("api/Rooms")]
        public IHttpActionResult GetRooms()
        {
            var query = new UIRoomQuery();

            return Ok(query.Search());
        }

        [HttpGet]
        [Route("api/Rooms/{id}")]
        public IHttpActionResult GetRoom(int id)
        {
            var query = new UIRoomQuery();

            return Ok(query.GetById(id));
        }
    }
}
