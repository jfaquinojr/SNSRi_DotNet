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
    public class RoomDeviceController : ApiController
    {

        [HttpPost]
        [Route("api/CreateRoomDevice")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateRoomDevice(UIRoomDevice roomDevice)
        {
            roomDevice.CreatedOn = DateTime.Now;
            var cmd = new UIRoomDeviceCommand();
            roomDevice.Id = cmd.Create(roomDevice);
            return Ok(roomDevice.Id);
        }

        [HttpPost]
        [Route("api/DeleteRoomDevice/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteRoomDevice(int id)
        {
            var cmd = new UIRoomDeviceCommand();
            cmd.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
