using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mailroom.Controllers
{
    public class RoutesController : Controller
    {
        //
        // GET: /Routes/

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Recepcion()
        {
            return View();
        }

        public ActionResult Proveedores()
        {
            return View();
        }

        public ActionResult ProveedoresAdd(int? id)
        {
            return View();
        }

        public ActionResult Etiqueta()
        {
            return View();
        }

        public ActionResult Egresos()
        {
            return View();
        }

        public ActionResult Reportes()
        {
            return View();
        }
    }
}