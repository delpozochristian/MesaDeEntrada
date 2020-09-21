using DB;
using DB.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace BL
{

    public class BLRecepcion
    {
        private const string UBICACION_LOG = "BLRecepcion";

        public GenericResponse<RecepcionResponse> Guardar(GuardarRecepcionRequest data)
        {
            GenericResponse<RecepcionResponse> response = new GenericResponse<RecepcionResponse>();

            try
            {
                response.Code = 200;

                Database db = new Database();

                Pedidos ingreso = new Pedidos();
                ingreso.CodigoBarra = data.NroSeguimiento;
                ingreso.IdDestinatario = data.IdDestinatario;
                ingreso.EscaneoDePieza = data.EscaneoSeguridad;
                ingreso.ImprimirEtiqueta = data.ImprimirEtiqueta;

                ingreso.Estado = Constants.Pedidos.ID_INGRESADO;
                ingreso.EstadoFecha = DateTime.Now;
                ingreso.EstadoPor = data.IdUsuario;
                ingreso.FechaImposicion = DateTime.Now;
                ingreso.IdSector = data.IdSectorDestinatario;
                ingreso.IdDireccion = data.IdDireccionDestinatario;
                ingreso.IdProveedor = data.IdProveedor;
                ingreso.IdCanalizacion = data.Canalizacion.Id;
                ingreso.IdTipoDeProducto = data.IdTipoProducto;
                ingreso.NroRemito = data.NroRemitoProveedor;
                ingreso.NroSeguimiento = data.NroSeguimiento;
                ingreso.Observacion = data.Observacion;
                ingreso.NroOrdeDeCompra = data.NroOrdenCompraProveedor;
                ingreso.UsuarioCreador = data.IdUsuario;


                if (string.IsNullOrEmpty(data.IdRemitente))
                {
                    data.IdRemitente = ConfigurationManager.AppSettings["ID_USUARIO_REMITENTE_MESA"];
                    Destinatarios usuarioRemitente = db.Destinatarios.Where(x => x.Id == data.IdRemitente).FirstOrDefault();

                    data.IdSectorRemitente = usuarioRemitente.IdSector;
                    data.IdSucursalRemitente = usuarioRemitente.IdSucursal;
                    data.IdDireccionRemitente = usuarioRemitente.IdBandeja;
                }

                ingreso.IdRemitente = (string.IsNullOrEmpty(data.IdRemitente) ? "-" : data.IdRemitente);
                ingreso.IdSectorRemitente = (string.IsNullOrEmpty(data.IdSectorRemitente) ? "-" : data.IdSectorRemitente);
                ingreso.IdSucursalRemitente = (string.IsNullOrEmpty(data.IdSucursalRemitente) ? "-" : data.IdSucursalRemitente);
                ingreso.IdDireccionRemitente = (string.IsNullOrEmpty(data.IdDireccionRemitente) ? "-" : data.IdDireccionRemitente);

                if (data.IdArchivo != 0)
                {
                    Archivos archivo = db.Archivos.First(x => x.Id == data.IdArchivo);
                    ingreso.Archivos.Add(archivo);
                }



                if (data.Canalizacion.Id != ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_COLABORADOR"] && 
                    data.Canalizacion.Id != ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_TERCERO"] && 
                    data.Canalizacion.Id != ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_LABORAL"])
                {
                    WSExterno.OCASA wsocasa = new WSExterno.OCASA();

                    ImponerWSRequest dataRequest = new ImponerWSRequest();

                    dataRequest.NroSeguimiento = data.NroSeguimiento;
                    dataRequest.Fecha = DateTime.Now.ToString("dd/MM/yyyy");
                    dataRequest.TipoProducto = data.IdTipoProducto.ToString();

                    dataRequest.Proveedor = new ImponerWSRequest.ImponerProveedorWSRequest();
                    dataRequest.Proveedor.Id = data.IdProveedor.ToString();
                    dataRequest.Proveedor.NroOrdenCompra = data.NroOrdenCompraProveedor;
                    dataRequest.Proveedor.NroRemito = data.NroRemitoProveedor;
                    dataRequest.Proveedor.RazonSocial = data.RazonSocialProveedor;
                    dataRequest.Proveedor.Cuit = data.CuitProveedor;

                    dataRequest.Remitente = new ImponerWSRequest.ImponerRemitenteWSRequest();
                    dataRequest.Remitente.Id = data.IdRemitente;
                    dataRequest.Remitente.Sector = data.IdSectorRemitente;
                    dataRequest.Remitente.Sucursal = data.IdSucursalRemitente;
                    dataRequest.Remitente.Bandeja = data.IdDireccionRemitente;

                    dataRequest.Destinatario = new ImponerWSRequest.ImponerDestinatarioWSRequest();
                    dataRequest.Destinatario.Id = data.IdDestinatario;
                    dataRequest.Destinatario.Bandeja = data.IdDireccionDestinatario;
                    dataRequest.Destinatario.Sector = data.IdSectorDestinatario;
                    dataRequest.Destinatario.Sucursal = data.IdSucursalDestinatario;

                    dataRequest.Observacion = data.Observacion;
                    dataRequest.Canalizacion = data.Canalizacion.Id.ToString();

                    WSLog wsExternos = wsocasa.Imponer(dataRequest);

                    ImponerWSResponse responseWs = ImponerWSResponse.Map(wsExternos.JSONResponse);

                    db.WSLog.Add(wsExternos);
                    db.SaveChanges();

                    if (wsExternos.Estado != "ERROR")
                    {
                        var codebar = responseWs.Pedidos[0].CodigoBarra;

                        if (string.IsNullOrEmpty(codebar))
                            codebar = ingreso.NroSeguimiento;

                        ingreso.CodigoBarra = codebar;
                    }

                    string jsonResponse = wsExternos.JSONResponse;

                }

                ingreso = db.Pedidos.Add(ingreso);
                db.SaveChanges();

                if (data.Canalizacion.Id == ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_COLABORADOR"] || 
                    data.Canalizacion.Id == ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_TERCERO"] ||
                    data.Canalizacion.Id == ConfigurationManager.AppSettings["ID_CANALIZACION_RETIRA_LABORAL"])
                {
                    Engresos egreso = new Engresos
                    {
                        Fecha = DateTime.Now
                    };
                    egreso = db.Engresos.Add(egreso);

                    db.SaveChanges();

                    EgresoPedidos egreped = new EgresoPedidos
                    {
                        IdEgreso = egreso.Id,
                        IdPedido = ingreso.Id
                    };

                    db.EgresoPedidos.Add(egreped);

                    ingreso.Estado = Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO;
                    ingreso.EstadoFecha = DateTime.Now;
                    ingreso.EstadoPor = data.IdUsuario;

                    ingreso.CodigoBarra = ingreso.Id.ToString().PadLeft(13, '0');

                    db.SaveChanges();
                }

                response.Result = new RecepcionResponse() { NroSeguimiento = ingreso.NroSeguimiento, IdIngreso = ingreso.Id, CodigoBarra = ingreso.CodigoBarra };
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = ex.Message;

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = UBICACION_LOG, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<RecepcionResponse> SubirArchivo(Stream inputStream, string name)
        {
            GenericResponse<RecepcionResponse> response = new GenericResponse<RecepcionResponse>();
            response.Result = new RecepcionResponse();
            response.Code = 200;
            response.Result.IdFile = 0;
            try
            {
                Database db = new Database();

                BinaryReader br = new BinaryReader(inputStream);
                long numBytes = inputStream.Length;
                byte[] buff = br.ReadBytes((int)numBytes);

                Archivos archivo = new Archivos() { ArchivoRecepcion = buff, Nombre = name };

                db.Archivos.Add(archivo);
                db.SaveChanges();

                response.Result.IdFile = archivo.Id;
                response.Result.NameFile = name;

            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al subir el archivo";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = UBICACION_LOG, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }



            return response;
        }

        public GenericResponse<ConsultarWSResponse> ConsultarSeguimiento(string nroSeguimiento)
        {
            GenericResponse<ConsultarWSResponse> response = new GenericResponse<ConsultarWSResponse>();

            response.Code = 200;

            try
            {
                WSExterno.OCASA wsocasa = new WSExterno.OCASA();

                WSLog wsExternos = wsocasa.Consultar(nroSeguimiento);

                Database db = new Database();
                db.WSLog.Add(wsExternos);
                db.SaveChanges();

                string jsonResponse = wsExternos.JSONResponse;

                var consultarResponse = ConsultarWSResponse.Map(jsonResponse);

                response.Result = consultarResponse;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Ocurrio un error al comunicarse con OCASA";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = UBICACION_LOG, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<RecepcionResponse> ImponerSeguimiento(Pedidos ingreso)
        {
            GenericResponse<RecepcionResponse> response = new GenericResponse<RecepcionResponse>();

            return response;
        }

        public GenericResponse<EgresoResponse> EntregarPedidos(List<IngresoResponse> egresos, int idUsuario)
        {
            GenericResponse<EgresoResponse> response = new GenericResponse<EgresoResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                Engresos egreso = new Engresos() { Fecha = DateTime.Now };

                db.Engresos.Add(egreso);

                foreach (var item in egresos)
                {
                    EgresoPedidos egresoPedido = new EgresoPedidos();
                    egresoPedido.Engresos = egreso;
                    egresoPedido.IdPedido = item.IdIngreso;

                    var pedido = db.Pedidos.Where(x => x.Id == item.IdIngreso).FirstOrDefault();
                    if (pedido == null)
                        throw new Exception("Pedido con Id " + item.IdIngreso + "no encontrado");
                    
                    pedido.Estado = Constants.Pedidos.ID_RETIRADO;

                    pedido.EstadoFecha = DateTime.Now;
                    pedido.EstadoPor = idUsuario;

                    db.EgresoPedidos.Add(egresoPedido);
                }

                db.SaveChanges();

                response.Result = new EgresoResponse() { IdEgreso = egreso.Id };
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Ocurrió un error al generar un egreso";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = UBICACION_LOG, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;


        }
    }
}
