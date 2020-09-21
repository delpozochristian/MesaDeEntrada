using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class ComprobanteEgresoController : Controller
    {
        // GET: ComprobanteEgreso
        public ActionResult Index(int id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Get(int id)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerEgreso(id);
            return Json(response);
        }
    }
}