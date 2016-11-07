using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNSRi.Web.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        public ActionResult Edit()
        {
            return PartialView();
        }
    }
}