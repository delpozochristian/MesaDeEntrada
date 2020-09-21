using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mailroom.Controllers
{
    public class SecurityController : Controller
    {
        // GET: Security
        public ActionResult Index()
        {
            Session.RemoveAll();
            return View();
        }
    }
}