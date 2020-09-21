using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB;
using BL;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Helper.SecurityHelper.CheckLogin(Session))
                return View("~/Views/Exception/Permisos.cshtml");

            return View();
        }
    }
}