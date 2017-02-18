﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SNSRi.Api.Models;
using SNSRi.Business;
using SNSRi.Repository;
using SNSRi.Repository.Query;
using SNSRi.Common;

namespace SNSRi.Api.Controllers
{
    public class AccountController : Controller
    {
        private IFactoryResetter _factoryReset;

        public AccountController(IFactoryResetter factoryReset)
        {
            this._factoryReset = factoryReset;
        }

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
                var user = new UserQuery().ValidateUser(model.Email, model.Password);

                if (null != user)
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

                    var url = Utility.GetConfig("HomeSeerURL", "http://localhost:8002");
                    _factoryReset.FactorySync();

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