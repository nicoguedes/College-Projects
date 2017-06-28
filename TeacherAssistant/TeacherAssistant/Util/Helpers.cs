using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;

namespace TeacherAssistant.Util
{
    public static class Helpers
    {
        public static IHtmlString GetPageUrl(this HtmlHelper htmlHelper, ViewContext viewContext)
        {
            StringBuilder urlBuilder = new StringBuilder();
            urlBuilder.Append("data-url='");
            urlBuilder.Append(viewContext.HttpContext.Request.Url.GetComponents(UriComponents.PathAndQuery, UriFormat.UriEscaped));
            urlBuilder.Append("'");
            return htmlHelper.Raw(urlBuilder.ToString());
        }
    }
}