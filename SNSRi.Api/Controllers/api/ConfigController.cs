using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SNSRi.Api.Controllers
{
    public class ConfigController : ApiController
    {
        [HttpGet]
        [Route("api/Config/HomeSeerUrl")]
        public IHttpActionResult HomeSeerUrl()
        {
            const string defaultUrl = "http://localhost:8002";
            var appSetting = ConfigurationManager.AppSettings["HomeSeerUrl"];
            if (string.IsNullOrEmpty(appSetting))
            {
                appSetting = defaultUrl;
            }

            return Ok(appSetting);
        }
    }
}
