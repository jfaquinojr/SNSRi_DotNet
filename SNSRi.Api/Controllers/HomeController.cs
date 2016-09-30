using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNSRi.Api.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";

			return View();
		}

		public ActionResult Devices(int roomId = 0)
		{
			if(roomId == 0)
				return new HttpNotFoundResult();
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
	}
}
