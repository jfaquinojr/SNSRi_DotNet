using SNSRi.Entities;
using SNSRi.Repository.Commands;
using SNSRi.Repository.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SNSRi.Api.Controllers
{
    public class CommandsController : ApiController
    {

        // POST: api/ChangeDeviceValue/5
        [HttpPost]
        [Route("api/ChangeDeviceValue/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult ChangeDeviceValue(int Id, Event entity)
        {
            var qryDevice = new DeviceQuery();
            var device = qryDevice.GetByReferenceId(Id);

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

        #region Template
        //// GET: api/Users2
        //public IQueryable<User> GetUsers()
        //{
        //    return db.Users;
        //}

        //// GET: api/Users2/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult GetUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(user);
        //}

        //// PUT: api/Users2/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutUser(int id, User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != user.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(user).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Users2
        //[ResponseType(typeof(User))]
        //public IHttpActionResult PostUser(User user)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Users.Add(user);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        //}

        //// DELETE: api/Users2/5
        //[ResponseType(typeof(User))]
        //public IHttpActionResult DeleteUser(int id)
        //{
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Users.Remove(user);
        //    db.SaveChanges();

        //    return Ok(user);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool UserExists(int id)
        //{
        //    return db.Users.Count(e => e.Id == id) > 0;
        //}
        #endregion
    }
}
