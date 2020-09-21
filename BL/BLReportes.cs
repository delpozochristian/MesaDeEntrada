using BarcodeLib;
using BL.Utilities;
using DB;
using DB.ViewModels;
using MvcPaging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;

namespace BL
{
    public class BLReportes
    {
        public GenericResponse<IngresosResponse> ObtenerIngresosPrefiltrado(string prefiltro, int pageIndex, int pageSize)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();

                switch (prefiltro)
                {
                    case "ingresos":
                        {
                            var pedidos = db.Pedidos.Where(x => x.Estado == Constants.Pedidos.ID_INGRESADO).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                            response.Result.ListaIngresos = IngresosResponse.MapList(pedidos.ToList());
                            response.Result.totalCount = pedidos.TotalItemCount.ToString();
                            response.Result.Prefiltros = new Prefiltrado() { EstadoPrefiltradoIngresos = EstadosResponse.Map(db.Estados.Where(x => x.Id == 1).FirstOrDefault()), Seleccionado = "ingresos" };
                            break;
                        }

                    case "egresos":
                        {
                            var pedidos = db.Pedidos.Where(x => x.Estado == Constants.Pedidos.ID_RETIRADO).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                            response.Result.ListaIngresos = IngresosResponse.MapList(pedidos.ToList());
                            response.Result.totalCount = pedidos.TotalItemCount.ToString();
                            response.Result.Prefiltros = new Prefiltrado() { EstadoPrefiltradoEgresos = EstadosResponse.Map(db.Estados.Where(x => x.Id == 4).FirstOrDefault()), Seleccionado = "egresos" };
                            break;
                        }

                    case "correspondencia":
                        {
                            var pedidos = db.Pedidos.Where(x => x.Canalizaciones.Id == "1").OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                            response.Result.ListaIngresos = IngresosResponse.MapList(pedidos.ToList());
                            response.Result.totalCount = pedidos.TotalItemCount.ToString();
                            response.Result.Prefiltros = new Prefiltrado() { CanalizacionesPrefiltradoCorrespondencia = CanalizacionesResponse.Map(db.Canalizaciones.Where(x => x.Id == "1").FirstOrDefault()), Seleccionado = "correspondencia" };
                            break;
                        }

                    case "paquetes":
                        {
                            var pedidos = db.Pedidos.Where(x => x.IdTipoDeProducto == 2).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                            response.Result.ListaIngresos = IngresosResponse.MapList(pedidos.ToList());
                            response.Result.totalCount = pedidos.TotalItemCount.ToString();
                            response.Result.Prefiltros = new Prefiltrado() { TipoProductoPrefiltradoPaquetes = TiposProductoResponse.Map(db.TipoDeProductos.Where(x => x.Id == 2).FirstOrDefault()), Seleccionado = "paquetes" };
                            break;
                        }

                }

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<IngresosResponse> ObtenerIngresoDetalle(int id)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();
                response.Result.ListaIngresos = new List<IngresoResponse>();

                response.Result.ListaIngresos.Add(IngresosResponse.MapDetail(db.Pedidos.Where(x=>x.Id == id).FirstOrDefault()));
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<IngresosResponse> ObtenerEgresosFilter(string nroSeguimiento, int tipoProducto, string sector, int proveedor, string destinatario, string fechaDesde, string fechaHasta, string canalizacion, string estado, string autorizado)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();

                if (String.IsNullOrEmpty(nroSeguimiento) && tipoProducto == 0 && String.IsNullOrEmpty(sector) && proveedor == 0 && String.IsNullOrEmpty(destinatario) && String.IsNullOrEmpty(fechaDesde) && String.IsNullOrEmpty(fechaHasta) && String.IsNullOrEmpty(estado) && String.IsNullOrEmpty(canalizacion) && String.IsNullOrEmpty(autorizado))
                {
                    response.Result.ListaIngresos = IngresosResponse.MapList(db.Pedidos.ToList());
                }
                else
                {
                    if (nroSeguimiento == null)
                        nroSeguimiento = "";

                    var query = from b in db.Pedidos
                                orderby b.FechaImposicion descending
                                where b.NroSeguimiento.Contains(nroSeguimiento)
                                && b.EgresoPedidos.Count > 0
                                select b;

                    if (tipoProducto != 0)
                        query = from b in query where b.TipoDeProductos.Id == tipoProducto select b;

                    if (!String.IsNullOrEmpty(sector))
                        query = from b in query where b.Sectores.Id == sector select b;

                    if (proveedor != 0)
                        query = from b in query where b.Proveedores.Id == proveedor select b;

                    if (!String.IsNullOrEmpty(destinatario))
                        query = from b in query where b.Destinatarios.Id == destinatario select b;

                    if (!String.IsNullOrEmpty(canalizacion))
                    {
                        List<string> idCanalizacion = canalizacion.Split(',').ToList();
                        query = from b in query where idCanalizacion.Contains(b.IdCanalizacion) select b;
                    }

                    if (!String.IsNullOrEmpty(autorizado))
                        query = from b in query where b.AutorizadoRetirar.Contains(autorizado) select b;

                    if (!String.IsNullOrEmpty(estado))
                    {
                        //int idEstado = int.Parse(estado);
                        List<int> idEstados = estado.Split(',').ToList().ConvertAll(int.Parse);
                        //var idEstados = new List<int>() { 1, 3 };
                        query = from b in query where idEstados.Contains(b.Estado) select b;
                    }

                    if (!String.IsNullOrEmpty(fechaDesde) && !String.IsNullOrEmpty(fechaHasta))
                    {
                        DateTime fechaSeleccioandaAn = DateTime.ParseExact(fechaDesde + " 00:00", "dd/MM/yyyy HH:mm", null);
                        DateTime fechaSeleccioandaDe = DateTime.ParseExact(fechaHasta + " 23:59", "dd/MM/yyyy HH:mm", null);
                        query = from b in query where b.FechaImposicion.Value < fechaSeleccioandaDe && b.FechaImposicion.Value > fechaSeleccioandaAn select b;
                    }

                    response.Result.ListaIngresos = IngresosResponse.MapList(query.ToList());
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los pedidos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<IngresosResponse> ObtenerIngresosFilterPaginado(int pageIndex, int pageSize, string nroSeguimiento, int tipoProducto, string sector, int proveedor, string destinatario, string fechaDesde, string fechaHasta, string canalizacion, string estado, string autorizado, string prefiltro)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();

                
                if (string.IsNullOrEmpty(prefiltro) && String.IsNullOrEmpty(nroSeguimiento) && tipoProducto == 0 && String.IsNullOrEmpty(sector) && proveedor == 0 && String.IsNullOrEmpty(destinatario) && String.IsNullOrEmpty(fechaDesde) && String.IsNullOrEmpty(fechaHasta) && String.IsNullOrEmpty(estado) && String.IsNullOrEmpty(canalizacion) && String.IsNullOrEmpty(autorizado))
                {
                    var pedidosTodos = db.Pedidos.OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidosTodos.ToList());
                    response.Result.totalCount = pedidosTodos.TotalItemCount.ToString();
                }
                else
                {
                    if (nroSeguimiento == null)
                        nroSeguimiento = "";

                    var query = db.Pedidos.AsQueryable();

                    if (!string.IsNullOrEmpty(nroSeguimiento))
                        query = query.Where(w => w.NroSeguimiento.Contains(nroSeguimiento));

                    query= query.OrderByDescending(o => o.FechaImposicion);

                    //var query = from b in db.Pedidos
                    //            orderby b.FechaImposicion descending
                    //            where b.NroSeguimiento.Contains(nroSeguimiento) || string.IsNullOrEmpty(b.NroSeguimiento)
                    //            select b;

                    if (tipoProducto != 0)
                        query = from b in query where b.TipoDeProductos.Id == tipoProducto select b;

                    if (!String.IsNullOrEmpty(sector))
                        query = from b in query where b.Sectores.Id == sector select b;

                    if (proveedor != 0)
                        query = from b in query where b.Proveedores.Id == proveedor select b;

                    if (!String.IsNullOrEmpty(destinatario))
                        query = from b in query where b.Destinatarios.Id == destinatario select b;

                    if (!String.IsNullOrEmpty(canalizacion))
                        query = from b in query where b.IdCanalizacion == canalizacion select b;

                    if (!String.IsNullOrEmpty(autorizado))
                        query = from b in query where b.AutorizadoRetirar.Contains(autorizado) select b;

                    if (!String.IsNullOrEmpty(estado))
                    {
                        //int idEstado = int.Parse(estado);
                        List<int> idEstados = estado.Split(',').ToList().ConvertAll(int.Parse);
                        //var idEstados = new List<int>() { 1, 3 };
                        query = from b in query where idEstados.Contains(b.Estado) select b;
                    }

                    if (!string.IsNullOrWhiteSpace(fechaDesde))
                    {
                        CultureInfo esAR = new CultureInfo("es-AR");
                        DateTime fechaSeleccioandaDesde = DateTime.ParseExact(fechaDesde + " 00:00", "dd/MM/yyyy HH:mm", esAR);
                        query = query.Where(w => w.FechaImposicion.Value > fechaSeleccioandaDesde);
                    }


                    if (!string.IsNullOrWhiteSpace(fechaHasta))
                    {
                        CultureInfo esAR = new CultureInfo("es-AR");
                        DateTime fechaSeleccioandaHasta = DateTime.ParseExact(fechaHasta + " 23:59", "dd/MM/yyyy HH:mm", esAR);
                        query = query.Where(w => w.FechaImposicion.Value < fechaSeleccioandaHasta);
                    }


                    if (!string.IsNullOrEmpty(prefiltro))
                    {

                        switch (prefiltro)
                        {
                            case "ingresos":
                                {
                                    var pedidosList = query.Where(x => x.Estado == Constants.Pedidos.ID_INGRESADO).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidosList.ToList());
                                    response.Result.totalCount = pedidosList.TotalItemCount.ToString();
                                    response.Result.Prefiltros = new Prefiltrado() { EstadoPrefiltradoIngresos = EstadosResponse.Map(db.Estados.Where(x => x.Id == 1).FirstOrDefault()), Seleccionado = "ingresos" };
                                    break;
                                }

                            case "egresos":
                                {
                                    var pedidosList = query.Where(x => x.Estado == Constants.Pedidos.ID_RETIRADO).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidosList.ToList());
                                    response.Result.totalCount = pedidosList.TotalItemCount.ToString();
                                    response.Result.Prefiltros = new Prefiltrado() { EstadoPrefiltradoEgresos = EstadosResponse.Map(db.Estados.Where(x => x.Id == 4).FirstOrDefault()), Seleccionado = "egresos" };
                                    break;
                                }

                            case "correspondencia":
                                {
                                    var pedidosList = query.Where(x => x.Canalizaciones.Id == "1").OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidosList.ToList());
                                    response.Result.totalCount = pedidosList.TotalItemCount.ToString();
                                    response.Result.Prefiltros = new Prefiltrado() { CanalizacionesPrefiltradoCorrespondencia = CanalizacionesResponse.Map(db.Canalizaciones.Where(x => x.Id == "1").FirstOrDefault()), Seleccionado = "correspondencia" };
                                    break;
                                }

                            case "paquetes":
                                {
                                    var pedidosList = query.Where(x => x.IdTipoDeProducto == 2).OrderByDescending(x => x.FechaImposicion).ToPagedList(pageIndex - 1, pageSize);
                                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidosList.ToList());
                                    response.Result.totalCount = pedidosList.TotalItemCount.ToString();
                                    response.Result.Prefiltros = new Prefiltrado() { TipoProductoPrefiltradoPaquetes = TiposProductoResponse.Map(db.TipoDeProductos.Where(x => x.Id == 2).FirstOrDefault()), Seleccionado = "paquetes" };
                                    break;
                                }
                        }
                        return response;
                    }


                    var pedidos = query.ToPagedList(pageIndex - 1, pageSize);
                    response.Result.ListaIngresos = IngresosResponse.MapList(pedidos.ToList());
                    response.Result.totalCount = pedidos.TotalItemCount.ToString();
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los pedidos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

       

        public GenericResponse<IngresosResponse> ObtenerIngresoUnico(string nroSeguimiento)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();

                response.Result.ListaIngresos = IngresosResponse.MapList(db.Pedidos.Where(x => x.NroSeguimiento == nroSeguimiento && x.Estado == Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO).ToList());

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        //RF
        public GenericResponse<IngresosResponse> ObtenerIngreso(string nroSeguimiento)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();

                if (String.IsNullOrEmpty(nroSeguimiento))
                    response.Result.ListaIngresos = IngresosResponse.MapList(db.Pedidos.Where(x => x.Estado == Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO).OrderBy(x => x.FechaImposicion).ToList());
                else
                    response.Result.ListaIngresos = IngresosResponse.MapList(db.Pedidos.Where(x => x.NroSeguimiento.Contains(nroSeguimiento) && x.Estado == Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO).ToList());

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<IngresosResponse> ObtenerIngreso(int id)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();
                response.Result.ListaIngresos = new List<IngresoResponse>();
                response.Result.ListaIngresos.Add(IngresosResponse.Map(db.Pedidos.Where(x => x.Id == id).First()));
                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<IngresosResponse> ObtenerEgreso(int id)
        {
            GenericResponse<IngresosResponse> response = new GenericResponse<IngresosResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new IngresosResponse();
                response.Result.ListaIngresos = new List<IngresoResponse>();

                List<EgresoPedidos> egresosPedidos = db.EgresoPedidos.Where(x => x.IdEgreso == id).ToList();

                foreach (var item in egresosPedidos)
                {
                    response.Result.ListaIngresos.Add(IngresosResponse.Map(item.Pedidos));
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<ArchivoIngresoResponse> ObtenerArchivoIngreso(int id)
        {
            GenericResponse<ArchivoIngresoResponse> response = new GenericResponse<ArchivoIngresoResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();
                response.Result = new ArchivoIngresoResponse();

                Pedidos ingreso = db.Pedidos.Where(x => x.Id == id).FirstOrDefault();

                Archivos archivo = ingreso.Archivos.FirstOrDefault();

                if (archivo != null)
                {
                    response.Result.IdIngreso = id;
                    response.Result.Data = archivo.ArchivoRecepcion;
                    response.Result.Nombre = archivo.Nombre;
                }

                return response;
            }
            catch (Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al traer los ingresos";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();
            }

            return response;
        }

        public GenericResponse<bool> Cancel(string codeBar, int idUsuario)
        {
            try
            {
                Database db = new Database();

                WSExterno.OCASA wsocasa = new WSExterno.OCASA();

                WSLog wsExternos = wsocasa.Cancelar(codeBar);

                db.WSLog.Add(wsExternos);
                db.SaveChanges();

                string jsonResponse = wsExternos.JSONResponse;
                dynamic jobject = JValue.Parse(jsonResponse);
                //jsonRespone["Status"]
                if (jobject.Status == true)
                {
                    Database context = new Database();
                    Pedidos pedidos = context.Pedidos.Where(x => x.CodigoBarra == codeBar).FirstOrDefault();
                    pedidos.Estado = Constants.Pedidos.ID_CANCELADO;

                    pedidos.EstadoFecha = DateTime.Now;
                    pedidos.EstadoPor = idUsuario;

                    pedidos.FechaCancelacion = DateTime.Now;
                    pedidos.CanceladoPor = idUsuario;
                    context.SaveChanges();

                    return new GenericResponse<bool>() { Code = 200, Result = true };

                }
                else
                {
                    return new GenericResponse<bool>() { Code = 500, Error = "No se puede cancelar el pedido" };
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_REPORTES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

        public string Obtenerbarcode(string cadena)
        {
            try
            {
                BarcodeLib.Barcode b = new Barcode
                {
                    IncludeLabel = true
                };
                //var saveTypes = BarcodeLib.SaveTypes.GIF;
                var type = BarcodeLib.TYPE.CODE128;
                var Forecolor = "000000";
                var Backcolor = "FFFFFF";
                Image barcodeImage = b.Encode(type, cadena, ColorTranslator.FromHtml("#" + Forecolor), ColorTranslator.FromHtml("#" + Backcolor), 500, 300);
                //return barcodeImage;

                Conversion b64 = new Conversion();

                string b64barcode = b64.ImageToBase64(barcodeImage, ImageFormat.Jpeg);

                return b64barcode;
                //System.IO.MemoryStream MemStream = new System.IO.MemoryStream();
                //barcodeImage.Save(MemStream, ImageFormat.Jpeg);
                //MemStream.WriteTo("C:\\Users\\Leonar\\Desktop\\zdfsddfasdas.gif");
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_REPORTES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return null;
            }

        }
    }
}
