using DB;
using System;
using System.Linq;

namespace WSInterno
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class NotificacionService : Notificacion
    {
        public string NotificarEstado(int estado, string codigoBarra)
        {
            return NotificarNuevoEstado(estado, codigoBarra, "usuario@servicio", null);
        }

        public string NotificarNuevoEstado(int estado, string codigoBarra, string email = "usuario@servicio", string fecha = null)
        {

            WSLog log = new WSLog();

            try
            {
                Database db = new Database();

                log = new WSLog() { FechaLlamada = DateTime.Now, FechaRespuesta = DateTime.Now, JSONRequest = "estado: " + estado.ToString() + "; codigoBarra: " + codigoBarra + "; email: " + email + "; fecha: " + fecha, JSONResponse = "En proceso", Servicio = "NotificacionEstado", Url = "http://externo" };
                db.WSLog.Add(log);
                db.SaveChanges();

                DateTime fechaOperacion = DateTime.Now;
                if (!string.IsNullOrEmpty(fecha))
                    fechaOperacion = DateTime.Parse(fecha);

                Pedidos pedido = db.Pedidos.Where(x => x.CodigoBarra == codigoBarra).FirstOrDefault();

                if (pedido != null)
                {
                    if (estado == Constants.Pedidos.ID_CANCELADO && pedido.Estado != Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO)//quieren Cancelar y está Disponible para retiro
                    {
                        log.JSONResponse = "El pedido no se encuentra en estado para ser cancelado.";
                    }
                    else
                    {
                        pedido.Estado = estado;

                        Usuarios usuario = db.Usuarios.Where(x => x.email == email).FirstOrDefault();
                        if (usuario == null)
                        {

                            usuario = new Usuarios()
                            {
                                email = email,
                                password = "INDEFINIDO"
                            };
                            db.Usuarios.Add(usuario);
                            db.SaveChanges();

                        }

                        pedido.CanceladoPor = usuario.id;
                        pedido.FechaCancelacion = fechaOperacion;

                        pedido.EstadoFecha = fechaOperacion;
                        pedido.EstadoPor = usuario.id;

                        log.JSONResponse = "Pedido modificado: " + pedido.Id;
                    }

                    log.FechaRespuesta = DateTime.Now;
                    db.SaveChanges();

                    return log.JSONResponse;
                }
                else
                {
                    log.FechaRespuesta = DateTime.Now;
                    log.JSONResponse = "El pedido " + codigoBarra + " no se encuentra en el sistema";
                    db.SaveChanges();

                    throw new Exception(log.JSONResponse);
                }

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = "WS", Mensaje = message, Detalle = messageInner });

                log.JSONResponse = "Error al cambiar estado:" + message;
                log.FechaRespuesta = DateTime.Now;

                db2.SaveChanges();

                return "Error al cambiar estado:" + message;
            }
        }

        public string NotificarPedido(Pedido pedido)
        {
            WSLog log = new WSLog();

            try
            {
                Database db = new Database();

                log = new WSLog() { FechaLlamada = DateTime.Now, FechaRespuesta = DateTime.Now, JSONRequest = pedido.ToString(), JSONResponse = "En proceso", Servicio = "Notificacion", Url = "http://externo" };
                db.WSLog.Add(log);
                db.SaveChanges();

                Pedidos ingreso = new Pedidos();

                ingreso.CodigoBarra = pedido.CodigoBarra;
                ingreso.Observacion = pedido.Observacion;
                //RF
                ingreso.Estado = Constants.Pedidos.ID_DISPONIBLE_PARA_RETIRO; //Disponible para retiro

                Usuarios usuario = db.Usuarios.Where(x => x.email == "usuario@servicio").FirstOrDefault();
                if (usuario != null)
                    ingreso.EstadoPor = usuario.id;
                ingreso.EstadoFecha = DateTime.Now;


                Direcciones direccion = db.Direcciones.Where(x => x.Id == pedido.Destinatario.Bandeja).FirstOrDefault();

                if (direccion != null)
                {
                    ingreso.IdDireccion = direccion.Id;
                }
                else
                {
                    direccion = new Direcciones();
                    direccion.Id = pedido.Destinatario.Bandeja;
                    direccion.Descripcion = pedido.Destinatario.DescripcionBandeja;

                    db.Direcciones.Add(direccion);

                    db.SaveChanges();

                    ingreso.IdDireccion = direccion.Id;
                }

                Sectores sector = db.Sectores.Where(x => x.Id == pedido.Destinatario.Sector).FirstOrDefault();

                if (sector != null)
                {
                    ingreso.IdSector = sector.Id;
                }
                else
                {
                    sector = new Sectores();
                    sector.Id = pedido.Destinatario.Sector;
                    sector.Descripcion = pedido.Destinatario.DescripcionSector;

                    db.Sectores.Add(sector);

                    db.SaveChanges();

                    ingreso.IdSector = sector.Id;
                }

                DB.Destinatarios destinatario = db.Destinatarios.Where(x => x.Id == pedido.Destinatario.Id).FirstOrDefault();

                if (destinatario != null)
                {
                    ingreso.IdDestinatario = destinatario.Id;
                }
                else
                {
                    destinatario = new DB.Destinatarios();
                    destinatario.Id = pedido.Destinatario.Id;
                    destinatario.IdBandeja = pedido.Destinatario.Bandeja;
                    destinatario.IdSector = pedido.Destinatario.Sector;
                    destinatario.IdCentroDeCostos = pedido.Destinatario.CentroDeCostos;
                    destinatario.NombreDeUsuario = pedido.Destinatario.Nombre;
                    destinatario.IdSucursal = pedido.Destinatario.Sucursal;
                    destinatario.DescripcionBandeja = pedido.Destinatario.DescripcionBandeja;
                    destinatario.DescripcionCentroDeCostos = pedido.Destinatario.DescripcionCentroCostos;
                    destinatario.DescripcionSector = pedido.Destinatario.DescripcionSector;

                    db.Destinatarios.Add(destinatario);

                    db.SaveChanges();

                    ingreso.Destinatarios = destinatario;
                }

                ingreso.NroOrdeDeCompra = pedido.NroDeOrdenCompra;
                ingreso.UsuarioCreador = 2;
                ingreso.IdTipoDeProducto = int.Parse(pedido.TipoProducto);
                ingreso.IdProveedor = int.Parse(pedido.Proveedor.Id);

                ingreso.NroSeguimiento = pedido.NroSeguimiento;
                ingreso.IdCanalizacion = pedido.Destinatario.Canalizacion;
                ingreso.IdRemitente = pedido.Remitente.Id;
                ingreso.IdSectorRemitente = pedido.Remitente.Sector;
                ingreso.IdDireccionRemitente = pedido.Remitente.Bandeja;
                ingreso.IdSucursalRemitente = pedido.Remitente.Sucursal;

                ingreso.NroRemito = "";
                ingreso.EscaneoDePieza = false;
                ingreso.ImprimirEtiqueta = false;
                ingreso.FechaImposicion = DateTime.Now;

                ingreso.AutorizadoRetirar = pedido.AutorizadoRetirar;

                log.FechaRespuesta = DateTime.Now;

                db.Pedidos.Add(ingreso);

                db.SaveChanges();

                log.JSONResponse = "Pedido: " + ingreso.Id;

                db.SaveChanges();

                if (pedido.Destinatario.Canalizacion == "3" || pedido.Destinatario.Canalizacion == "5")
                {
                    DB.Engresos egreso = new DB.Engresos();
                    egreso.Fecha = DateTime.Now;
                    egreso = db.Engresos.Add(egreso);

                    db.SaveChanges();

                    DB.EgresoPedidos egreped = new DB.EgresoPedidos();
                    egreped.IdEgreso = egreso.Id;
                    egreped.IdPedido = ingreso.Id;

                    db.EgresoPedidos.Add(egreped);

                    db.SaveChanges();
                }

                return "Pedido: " + ingreso.Id;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                Database db2 = new Database();
                db2.Log.Add(new Log() { Fecha = DateTime.Now, Ubicacion = "WS", Mensaje = message, Detalle = messageInner });

                log.JSONResponse = "Error al generar Pedido:" + message;
                log.FechaRespuesta = DateTime.Now;

                db2.SaveChanges();

                return "Error al generar Pedido:" + message;
            }
        }
    }
}
