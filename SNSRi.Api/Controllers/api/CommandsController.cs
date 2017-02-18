using SNSRi.Entities;
using SNSRi.Repository.Commands;
using SNSRi.Repository.Query;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using SNSRi.Business;
using SNSRi.Entities.HomeSeer;
using SNSRi.Repository;
using SNSRi.Common;

namespace SNSRi.Api.Controllers
{
    //[Authorize]
    public class CommandsController : ApiController
    {
        private IFactoryResetter _factoryReset;
        private IDeviceRepository _deviceRepository;

        public CommandsController(IFactoryResetter factoryReset, IDeviceRepository deviceRepository)
        {
            _factoryReset = factoryReset;
            _deviceRepository = deviceRepository;
        }

        // POST: api/ChangeDeviceValue/5
        [HttpPost]
        [Route("api/ChangeDeviceValue/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeDeviceValue(int Id, Event entity)
        {
            var device = _deviceRepository.GetByReferenceId(Id);

            if (device == null)
            {
                var httpError = new HttpError($"Device with ReferenceId: {Id} was not found");
                var errorResponse = Request.CreateErrorResponse(HttpStatusCode.NotFound, httpError);
                throw new HttpResponseException(errorResponse);
            }

            var cmdEvent = new EventCommand();
            cmdEvent.Create(new Event(device.Id, entity.NewStatus, device.Status, entity.OccurredOn));

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/CreateActivity")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CreateActivity(Activity activity)
        {
            activity.CreatedOn = DateTime.Now;
            var cmd = new ActivityCommand();
            activity.Id = cmd.Create(activity);
            return Ok(activity.Id);
        }

        [HttpPost]
        [Route("api/CreateUser")]
        [ResponseType(typeof(int))]
        public IHttpActionResult CreateUser(User user)
        {
            user.CreatedOn = DateTime.Now;
            var cmd = new UserCommand();
            user.Id = cmd.Create(user);
            return Ok(user.Id);
        }

        [HttpPost]
        [Route("api/UpdateUser")]
        [ResponseType(typeof(void))]
        public IHttpActionResult UpdateUser(User user)
        {
            user.ModifiedOn = DateTime.Now;
            var cmd = new UserCommand();
            cmd.Update(user);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/DeleteUser/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult DeleteUser(int id)
        {
            var cmd = new UserCommand();
            cmd.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }


        [HttpPost]
        [Route("api/CloseTicket")]
        [ResponseType(typeof(void))]
        public IHttpActionResult CloseTicket(Activity activity)
        {
            CreateActivity(activity);

            var cmd = new TicketCommand();
            cmd.CloseTicket(activity.TicketId);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/FactoryReset")]
        public IHttpActionResult FactoryReset()
        {
            _factoryReset.FactoryReset();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("api/FactorySync")]
        public IHttpActionResult FactorySync()
        {
            _factoryReset.FactorySync();
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
