using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNSRi.Web.Controllers
{
    [Authorize]
    public class ResidentsController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }

        public ActionResult RoomResidents()
        {
            return PartialView();
        }
    }
}