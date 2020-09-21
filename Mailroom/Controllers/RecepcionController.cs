using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace Mailroom.Controllers
{
    public class RecepcionController : Controller
    {
        // GET: Recepcion

        [HttpPost]
        public JsonResult Guardar(string nroSeguimiento, Proveedores proveedores, Destinatarios destinatario, Sectores sector, Direcciones direccion, 
            Canalizaciones canalizacion, string nroRemito, string nroOrdenCompra, 
            int tipoProducto, bool escaneoPieza, bool imprimirEtiqueta, int idArchivo, string observacion,
            string IdRemitente, string IdDireccionRemitente, string IdSectorRemitente, string IdSucursalRemitente)
        {
            BL.BLRecepcion blRecepcion = new BL.BLRecepcion();

            BL.GuardarRecepcionRequest data = new BL.GuardarRecepcionRequest();
            
            data.NroSeguimiento = nroSeguimiento;
            data.EscaneoSeguridad = escaneoPieza;
            data.Canalizacion = canalizacion;
            data.IdArchivo = idArchivo;
            data.IdTipoProducto = tipoProducto;
            data.IdUsuario = Helper.SecurityHelper.GetLogin(Session).IdUsuario;

            data.IdProveedor = proveedores.Id;
            data.NroRemitoProveedor = nroRemito;
            data.NroOrdenCompraProveedor = nroOrdenCompra;
            data.RazonSocialProveedor = proveedores.RazonSocial;
            data.CuitProveedor = proveedores.Cuit;

            data.IdDestinatario = destinatario.Id;
            data.IdDireccionDestinatario = direccion.Id;
            data.IdSectorDestinatario = sector.Id;
            data.IdSucursalDestinatario = destinatario.IdSucursal;

            data.IdRemitente = IdRemitente;
            data.IdDireccionRemitente = IdDireccionRemitente;
            data.IdSectorRemitente = IdSectorRemitente;
            data.IdSucursalRemitente = IdSucursalRemitente;

            data.Observacion = observacion;

            var response = blRecepcion.Guardar(data);
            
            return Json(response);
        }

        [WebMethod]
        public JsonResult SubirArchivo(HttpPostedFileBase file)
        {
            var inputStream = file.InputStream;
            BL.BLRecepcion blRecepcion = new BL.BLRecepcion();

            var response = blRecepcion.SubirArchivo(inputStream, file.FileName);

            return Json(response);
        }

        [HttpPost]
        public JsonResult Consultar(string nroSeguimiento)
        {
            BL.BLRecepcion blRecepcion = new BL.BLRecepcion();
            var response = blRecepcion.ConsultarSeguimiento(nroSeguimiento);



            return Json(response);
        }
    }
}