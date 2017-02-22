using KalikoCMS.Mvc.Framework;
using SNSRi.Web.PageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SNSRi.Web.Controllers
{
    public class ArticlePageController : PageController<ArticlePageType>
    {
        public override ActionResult Index(ArticlePageType currentPage)
        {
            return View(currentPage);
        }

    }
}