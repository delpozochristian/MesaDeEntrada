using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DB.ViewModels;
using System.Configuration;
using BL;
using DB;

namespace Mailroom.Controllers
{
    public class EgresosController : Controller
    {
        // GET: Egresos
        [HttpPost]
        public JsonResult GetIngreso(string nroSeguimiento)
        {
            BLReportes blReportes = new BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerIngreso(nroSeguimiento);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetIngresoUnico(string nroSeguimiento)
        {
            BLReportes blReportes = new BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerIngresoUnico(nroSeguimiento);
            return Json(response);
        }

        [HttpPost]
        public JsonResult GetIngresos(string nroSeguimiento, int proveedor, string destinatario, string fechaDesde, string fechaHasta, string autorizado)
        {
            var estadoParaEgresos = Constants.Pedidos.ID_INGRESADO + "," + Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO; //"1,3" //Configurar con el web config
            //var estadoParaEgresos = new List<int>() { 1, 3 };
            var canalizacion = ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_COLABORADOR"]+ ","+ ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_TERCERO"] + "," + ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_LABORAL"];
            BLReportes blReportes = new BLReportes();
            GenericResponse<IngresosResponse> response = blReportes.ObtenerEgresosFilter(nroSeguimiento, 0, null, proveedor, destinatario, fechaDesde, fechaHasta, canalizacion.ToString(), estadoParaEgresos.ToString(), autorizado);
            return Json(response);
        }

        [HttpPost]
        public JsonResult EntregarPedidos(List<IngresoResponse> ingresos)
        {
            BLRecepcion recepcion = new BLRecepcion();
            int IdUsuario = Helper.SecurityHelper.GetLogin(Session).IdUsuario;
            GenericResponse<EgresoResponse> response = recepcion.EntregarPedidos(ingresos, IdUsuario);

            return Json(response);
        }
    }
}