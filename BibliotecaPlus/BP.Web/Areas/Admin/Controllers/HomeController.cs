using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BP.Web.Models;

namespace BP.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [BPAuthorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
