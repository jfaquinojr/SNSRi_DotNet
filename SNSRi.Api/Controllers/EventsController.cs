using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Mvc;

namespace SNSRi.Api.Controllers
{
    public class EventsController : ApiController
    {

        [HttpPost]
        public void ChangeDeviceValue(int referenceId, string newStatus, DateTime changedOn)
        {

        }

    }
}
