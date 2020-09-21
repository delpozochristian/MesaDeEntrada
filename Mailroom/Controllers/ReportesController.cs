using DB.ViewModels;
using Mailroom.Helper;
using System.Linq;
using System.Web.Mvc;

namespace Mailroom.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes

        [HttpPost]
        public JsonResult GetIngresosFilterPaginado(int pageIndex, int pageSize, string nroSeguimiento, int tipoProducto, string sector, int proveedor, string destinatario, string fechaDesde, string fechaHasta, string canalizacion, string estado, string autorizado, string prefiltrado)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerIngresosFilterPaginado(pageIndex, pageSize, nroSeguimiento, tipoProducto, sector, proveedor, destinatario, fechaDesde, fechaHasta, canalizacion, estado, autorizado, prefiltrado);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetIngresoDetalle(int idDetalle)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerIngresoDetalle(idDetalle);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Cancel(string codeBar)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            var idUsuario = Helper.SecurityHelper.GetLogin(Session).IdUsuario;
            GenericResponse<bool> response = blReportes.Cancel(codeBar, idUsuario);
            return Json(response);
        }

        public ActionResult GetIngresosFilterExcel(string nroSeguimiento, int tipoProducto, string sector, int proveedor, string destinatario, string fechaDesde, string fechaHasta, string canalizacion, string estado, string autorizado, string prefiltrado)
        {

            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<IngresosResponse> ingresosResponse = blReportes.ObtenerIngresosFilterPaginado(1, int.MaxValue, nroSeguimiento, tipoProducto, sector, proveedor, destinatario, fechaDesde, fechaHasta, canalizacion, estado, autorizado, prefiltrado);
            var data = ingresosResponse.Result.ListaIngresos.ToArray<IngresoResponseVisible>();
            Response.AddHeader("content-disposition", "attachment;filename=Ingresos.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            return View(data);
        }
    }
}