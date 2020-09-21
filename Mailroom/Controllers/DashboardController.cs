using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard

        [HttpPost]
        public JsonResult GetDashboard(string search)
        {
            BL.BLDashboard blDashboard = new BL.BLDashboard();
            GenericResponse<DashboardResponse> response = blDashboard.ObtenerDashboard();
            return Json(response);
        }
    }
}