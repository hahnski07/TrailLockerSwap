using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace TrailLocker.Controllers
{
    public class logoutController : Controller
    {
        //
        // GET: /logout/

        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index", "home");
        }

    }
}
