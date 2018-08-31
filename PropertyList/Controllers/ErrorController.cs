using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PropertyList.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}