using DB.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mailroom.Controllers
{
    public class CombosController : Controller
    {
        const string HABILITADOS = "Habilitados";
        const string DESHABILITADOS = "Deshabilitados";
        const string TODOS = "Todos";

        [HttpPost]
        public JsonResult GetEstadosProveedor()
        {
            List<string> listEstados = new List<string>
            {
                HABILITADOS,
                DESHABILITADOS,
                TODOS
            };

            GenericResponse<List<string>> response = new GenericResponse<List<string>>() { 
                Code = 200, 
                Result = listEstados 
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetProveedores(string search)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<ProveedoresResponse> response = blProveedores.GetCombo(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetProveedoresReportes(string search)
        {
            BL.BLProveedores blProveedores = new BL.BLProveedores();
            GenericResponse<ProveedoresResponse> response = blProveedores.GetComboReportes(search);
            return Json(response);
        }
        
        [HttpPost]
        public JsonResult GetCanalizacion(string search)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<CanalizacionesResponse> response = blDestinatarios.GetComboCanalizacion(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDestinatarios(string search)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DestinatariosResponse> response = blDestinatarios.GetComboDestinatarios(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDestinatariosBySector(string search, string idSector)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DestinatariosResponse> response = blDestinatarios.GetComboDestinatariosBySector(search, idSector);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDestinatariosByDireccion(string search, string idDireccion)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DestinatariosResponse> response = blDestinatarios.GetComboDestinatariosByDireccion(search, idDireccion);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetSectores(string search)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<SectoresResponse> response = blDestinatarios.GetComboSectores(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetSectoresByDireccion(string search, string idDireccion)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<SectoresResponse> response = blDestinatarios.GetComboSectoresByDireccion(search, idDireccion);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetEstados(string search)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<EstadosResponse> response = blDestinatarios.GetComboEstados(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDirecciones(string search)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DireccionesResponse> response = blDestinatarios.GetComboDirecciones(search);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDireccionesBySector(string search, string idSector)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DireccionesResponse> response = blDestinatarios.GetComboDireccionesBySector(search, idSector);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDestinatario(string id)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DestinatarioResponse> response = blDestinatarios.GetDestinatario(id);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetSector(string id)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<SectorResponse> response = blDestinatarios.GetSector(id);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetDireccion(string id)
        {
            BL.BLDestinatarios blDestinatarios = new BL.BLDestinatarios();
            GenericResponse<DireccionResponse> response = blDestinatarios.GetDireccion(id);
            return Json(response);
        }
    }
}