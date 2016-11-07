using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SNSRi.Entities;
using SNSRi.Repository.Commands;
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

        [HttpPost]
        [Route("api/CreateRoom")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateRoom(UIRoom room)
        {
            room.CreatedOn = DateTime.Now;
            var cmd = new UIRoomCommand();
            room.Id = cmd.Create(room);
            return Ok(room.Id);
        }

        [HttpPost]
        [Route("api/UpdateRoom")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateRoom(UIRoom room)
        {
            room.ModifiedOn = DateTime.Now;
            var cmd = new UIRoomCommand();
            cmd.Update(room);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteRoom/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteRoom(int id)
        {
            var cmd = new UIRoomCommand();
            cmd.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
