using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SNSRi.Repository.Query;

namespace SNSRi.Api.Controllers.api
{
    public class DevicesController : ApiController
    {
        [HttpGet]
        [Route("api/Devices")]
        public IHttpActionResult GetDevices()
        {
            var query = new DeviceQuery();

            return Ok(query.Search());
        }

        [HttpGet]
        [Route("api/Rooms/{roomId}/Devices")]
        public IHttpActionResult GetDevicesByRoom(int roomId)
        {
            var query = new DeviceQuery();

            return Ok(query.GetByRoomId(roomId));
        }
    }
}
