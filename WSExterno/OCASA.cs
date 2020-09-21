using DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WSExterno
{
    public class OCASA
    {
        public WSLog Consultar(string nroSeguimiento)
        {
            try
            {
                string USER = ConfigurationManager.AppSettings["USER_WSOCASA"];

                DateTime fechaInicio = DateTime.Now;

                string PASSWORD = ConfigurationManager.AppSettings["PASSWORD_WSOCASA"];

                string json = "{\"security\":{\"login\":\""
                    + USER + "\",\"password\":\""
                    + PASSWORD + "\"},\"pedidos\":[{\"nroseguimiento\":\""
                    + nroSeguimiento + "\"}]}";

                string url = ConfigurationManager.AppSettings["WSOCASA"] + "consultar";

                string response = "";

                ServiceCaller sc = new ServiceCaller();

                HttpWebResponse webResponse = sc.POST(url, json);

                using (var reader = new System.IO.StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }

                DateTime fechaFin = DateTime.Now;

                WSLog wsExternos = new WSLog()
                {
                    FechaLlamada = fechaInicio,
                    FechaRespuesta = fechaFin,
                    JSONRequest = json,
                    JSONResponse = response,
                    Servicio = "Consultar",
                    Url = url
                };

                return wsExternos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WSLog Imponer(DB.ViewModels.ImponerWSRequest data)
        {
            DateTime fechaInicio = DateTime.Now;
            DateTime fechaFin = DateTime.Now;
            string USER = ConfigurationManager.AppSettings["USER_WSOCASA"];

            string PASSWORD = ConfigurationManager.AppSettings["PASSWORD_WSOCASA"];

            string json = "{\"security\":{\"login\":\""
                + USER + "\",\"password\":\""
                + PASSWORD + "\"},\"pedidos\":[" + this.GetPedidoJSON(data) + "]}";

            string url = ConfigurationManager.AppSettings["WSOCASA"] + "imponer";

            string response = "";

            WSLog wsExternos = new WSLog()
            {
                FechaLlamada = fechaInicio,
                FechaRespuesta = fechaFin,
                JSONRequest = json,
                JSONResponse = response,
                Servicio = "Imponer",
                Url = url
            };

            try
            {
                ServiceCaller sc = new ServiceCaller();

                HttpWebResponse webResponse = sc.POST(url, json);

                using (var reader = new System.IO.StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }

                if (response == "")
                    response = "(Vacio)";

                wsExternos.JSONResponse = response;
                wsExternos.FechaRespuesta = DateTime.Now;
                wsExternos.Estado = "OK";

                return wsExternos;

            }
            catch (Exception ex)
            {
                wsExternos.JSONResponse = ex.Message;
                wsExternos.Estado = "ERROR";
                return wsExternos;
            }
        }

        public WSLog Cancelar(string pedido)
        {
            try
            {
                string USER = ConfigurationManager.AppSettings["USER_WSOCASA"];

                DateTime fechaInicio = DateTime.Now;

                string PASSWORD = ConfigurationManager.AppSettings["PASSWORD_WSOCASA"];

                string json = "{\"security\":{\"login\":\""
                    + USER + "\",\"password\":\""
                    + PASSWORD + "\"},\"Pedido\":\"" + pedido + "\"}";

                string url = ConfigurationManager.AppSettings["WSOCASA"] + "cancelar";

                string response = "";

                ServiceCaller sc = new ServiceCaller();

                HttpWebResponse webResponse = sc.POST(url, json);

                using (var reader = new System.IO.StreamReader(webResponse.GetResponseStream()))
                {
                    response = reader.ReadToEnd();
                }

                DateTime fechaFin = DateTime.Now;

                WSLog wsExternos = new WSLog()
                {
                    FechaLlamada = fechaInicio,
                    FechaRespuesta = fechaFin,
                    JSONRequest = json,
                    JSONResponse = response,
                    Servicio = "Cancelar",
                    Url = url
                };

                return wsExternos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetPedidoJSON(DB.ViewModels.ImponerWSRequest data)
        {
            string pedido = "{\"nroseguimiento\":\"" + data.NroSeguimiento + "\"," +
                     "\"fecha\":\"" + data.Fecha + "\"," +
                     "\"tipoproducto\":\"0000" + data.TipoProducto + "\"," +
                     "\"proveedor\":" +
                     "{\"idproveedor\":\"" + data.Proveedor.Id + "\",\"razonsocial\":\"" + data.Proveedor.RazonSocial + "\",\"cuit\":\"" + data.Proveedor.Cuit + "\",\"nroremito\":\"" + data.Proveedor.NroRemito + "\",\"ordencompra\":\"" + data.Proveedor.NroOrdenCompra + "\"}," +
                     "\"remitente\":{" +
                     "\"idusuario\":\"" + data.Remitente.Id + "\",\"sucursal\":\"" + data.Remitente.Sucursal + "\",\"bandeja\":\"" + data.Remitente.Bandeja + "\",\"sector\":\"" + data.Remitente.Sector + "\"" +
                     "},\"destinatario\":{" +
                     "\"idusuario\":\"" + data.Destinatario.Id + "\",\"sucursal\":\"" + data.Destinatario.Sucursal + "\",\"bandeja\":\"" + data.Destinatario.Bandeja + "\",\"sector\":\"" + data.Destinatario.Sector + "\"" +
                     "},\"observacion\":\"" + data.Observacion + "\",\"canalizacion\":\"" + data.Canalizacion + "\"}";

            return pedido;
        }

        public void TestServicio()
        {
            try
            {
                NotificarPedido.NotificacionClient sv = new NotificarPedido.NotificacionClient();
                sv.NotificarPedido(new NotificarPedido.Pedido()
                {
                    NroSeguimiento = "TESTASD",
                    Observacion = "Test",
                    NroDeOrdenCompra = "123123",
                    Fecha = DateTime.Now.ToShortDateString(),
                    TipoProducto = "01",
                    Remitente = new NotificarPedido.ImponerRemitenteWSRequest() { Bandeja = "690000000064001000000000000000000000", Id = "014-ANEXO RENTAS", Sector = "EXTDGRP", Sucursal = "2810102000317" },
                    Destinatario = new NotificarPedido.ImponerDestinatarioWSRequest() { Bandeja = "690000000064001000000000000000000000", Id = "014-ANEXO RENTAS", Sector = "EXTDGRP", Sucursal = "2810102000317", Canalizacion = "1", DescripcionBandeja = "POSADAS, SAN MARTIN 1754 2° Y 3° P", DescripcionCentroCostos = "99999996 - EXTERNOS", DescripcionSector = "ANEXO RENTAS - POSADAS", CentroDeCostos = "999999960102000000", Nombre = "014-ANEXO RENTAS (DGR)" },
                    Proveedor = new NotificarPedido.ImponerProveedorWSRequest() { Cuit = "34-095626-6", NroOrdenCompra = "123456", NroRemito = "123456", RazonSocial = "OCASA", Id = "0" }
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
