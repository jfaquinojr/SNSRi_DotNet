using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNSRi.Api.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		public ActionResult Devices()
		{
			return PartialView();
		}

		public ActionResult Rooms()
		{
			return PartialView();
		}


		public ActionResult Toolbox()
		{
			return PartialView();
		}

		public ActionResult RoomTile()
		{
			return PartialView();
		}

        public ActionResult DeviceTile()
        {
            return PartialView();
        }

        public ActionResult Oops()
        {
            return PartialView();
        }

        public ActionResult EventsCharm()
        {
            return PartialView();
        }

        public ActionResult EventTile()
        {
            return PartialView();
        }

        public ActionResult PopupShowActivities()
        {
            return PartialView();
        }
    }
}
