using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SNSRi.Api.Models;

namespace SNSRi.Api.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(SimpleLoginModel model)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Email, model.Password))
                {
                    var identity = new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.Name, model.Email),
                            new Claim(ClaimTypes.Email, model.Email),
                            new Claim(ClaimTypes.Country, "USA")
                        },
                        "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return Redirect(Url.Action("index", "home"));
                }
                ModelState.AddModelError("", "Invalid username and password combination");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut();
            return RedirectToAction("Login");
        }

    }
}