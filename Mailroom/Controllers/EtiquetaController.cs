using DB.ViewModels;
using System.Web.Mvc;

namespace Mailroom.Controllers
{
    public class EtiquetaController : Controller
    {
        // GET: Etiqueta
        public ActionResult Index(int idIngreso)
        {
            return View();
        }

        [HttpPost]
        public JsonResult Obtener(int idIngreso)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerIngreso(idIngreso);

            return Json(response);
        }
        [HttpGet]
        public string ObtenerBarcode(string barcode)
        {
            BL.BLReportes blReportes = new BL.BLReportes();
            var response = blReportes.Obtenerbarcode(barcode);
            return response;
        }
    }
}