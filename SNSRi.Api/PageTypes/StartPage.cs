using KalikoCMS.Attributes;
using KalikoCMS.Core;
using KalikoCMS.PropertyType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SNSRi.Web.PageTypes
{
    [PageType("StartPage", "Article page", "TODO")]
    public class StartPageType : CmsPage
    {
        [Property("Article heading")]
        public virtual StringProperty Heading { get; set; }

        [ImageProperty("Top image", Width = 960, Height = 280)]
        public virtual ImageProperty TopImage { get; set; }

        [Property("Preamble")]
        public virtual TextProperty Preamble { get; set; }

        [Property("Main body")]
        public virtual HtmlProperty MainBody { get; set; }
    }
}