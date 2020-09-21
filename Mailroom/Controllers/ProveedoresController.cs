using System.Web.Mvc;
using DB.ViewModels;

namespace Mailroom.Controllers
{
    public class ProveedoresController : Controller
    {
        // GET: Proveedores

        [HttpPost]
        public JsonResult GetAll(int pageIndex, int pageSize, string razonSocial, string cuit, string estado, string fechaDesde, string fechaHasta)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<ProveedoresResponse> response = blProveedores.GetAll(pageIndex, pageSize, razonSocial, cuit, estado, fechaDesde, fechaHasta);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Get(int id)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<ProveedoresResponse> response = blProveedores.Get(id);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Save(string RazonSocial, string Cuit)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<bool> response = blProveedores.Save(null, RazonSocial, Cuit);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Edit(int? Id, string RazonSocial, string Cuit)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<bool> response = blProveedores.Save(Id, RazonSocial, Cuit);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<bool> response = blProveedores.Delete(id);
            return Json(response);
        }

        [HttpPost]
        public JsonResult Enable(int id)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<bool> response = blProveedores.Enable(id);
            return Json(response);
        }
    }
}