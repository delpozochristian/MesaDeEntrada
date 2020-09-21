using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.ViewModels;
using DB;

namespace BL
{
    public class BLDestinatarios
    {
        

        public GenericResponse<DestinatariosResponse> GetComboDestinatarios(string param)
        {
            try
            {
                IQueryable<Destinatarios> query = null;

                Database context = new Database();

                query = from b in context.Destinatarios
                        where b.NombreDeUsuario.Contains(param)
                        select b;

                if (String.IsNullOrEmpty(param))
                {
                    query = from b in context.Destinatarios
                            select b;
                }

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Result = new DestinatariosResponse() { ListaDestinatarios = DestinatariosResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<SectoresResponse> GetComboSectores(string param)
        {
            try
            {
                IQueryable<Sectores> query = null;

                Database context = new Database();


                query = from b in context.Sectores
                        where b.Descripcion.Contains(param)
                        select b;

                if (String.IsNullOrEmpty(param))
                {
                    query = from b in context.Sectores
                            select b;
                }


                return new GenericResponse<SectoresResponse>() { Code = 200, Result = new SectoresResponse() { ListaSectores = SectoresResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<SectoresResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<DireccionesResponse> GetComboDirecciones(string param)
        {
            try
            {
                IQueryable<Direcciones> query = null;

                Database context = new Database();

                query = from b in context.Direcciones
                        where b.Descripcion.Contains(param)
                        select b;

                if (String.IsNullOrEmpty(param))
                {
                    query = from b in context.Direcciones
                            select b;
                }


                return new GenericResponse<DireccionesResponse>() { Code = 200, Result = new DireccionesResponse() { ListaDirecciones = DireccionesResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<DireccionesResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<EstadosResponse> GetComboEstados(string param)
        {
            try
            {
                IQueryable<Estados> query = null;

                Database context = new Database();

                if (param == null)
                    param = "";

                query = from b in context.Estados
                        where b.Nombre.Contains(param)
                        select b;

                return new GenericResponse<EstadosResponse>() { Code = 200, Result = new EstadosResponse() { ListaEstados = EstadosResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<EstadosResponse>() { Code = 200, Error = ex.Message };
            }
        }

        public GenericResponse<CanalizacionesResponse> GetComboCanalizacion(string param)
        {
            try
            {
                IQueryable<Canalizaciones> query = null;

                Database context = new Database();

                if (param == null)
                    param = "";

                query = from b in context.Canalizaciones
                        where b.Descripcion.Contains(param)
                        select b;

                return new GenericResponse<CanalizacionesResponse>() { Code = 200, Result = new CanalizacionesResponse() { ListaCanalizaciones = CanalizacionesResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<CanalizacionesResponse>() { Code = 200, Error = ex.Message };
            }
        }

        public GenericResponse<DestinatarioResponse> GetDestinatario(string id)
        {
            GenericResponse<DestinatarioResponse> response = new GenericResponse<DestinatarioResponse>();
            response.Code = 200;

            try
            {
                IQueryable<Destinatarios> query = null;
                Database context = new Database();
                query = from b in context.Destinatarios
                        where b.Id == id
                        select b;
                response.Result = DestinatariosResponse.Map(query.FirstOrDefault());
                return response;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                response.Code = 500;
                response.Error = ex.Message;
                return response;
            }

        }

        public GenericResponse<SectorResponse> GetSector(string id)
        {
            GenericResponse<SectorResponse> response = new GenericResponse<SectorResponse>();
            response.Code = 200;

            try
            {
                IQueryable<Sectores> query = null;
                Database context = new Database();
                query = from b in context.Sectores
                        where b.Id == id
                        select b;
                response.Result = SectoresResponse.Map(query.FirstOrDefault());
                return response;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                response.Code = 500;
                response.Error = ex.Message;
                return response;
            }

        }

        public GenericResponse<DireccionResponse> GetDireccion(string id)
        {
            GenericResponse<DireccionResponse> response = new GenericResponse<DireccionResponse>();
            response.Code = 200;

            try
            {
                IQueryable<Direcciones> query = null;
                Database context = new Database();
                query = from b in context.Direcciones
                        where b.Id == id
                        select b;
                response.Result = DireccionesResponse.Map(query.FirstOrDefault());
                return response;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                response.Code = 500;
                response.Error = ex.Message;
                return response;
            }

        }

        public GenericResponse<DestinatariosResponse> GetComboDestinatariosBySector(string param, string idSector)
        {
            try
            {
                IQueryable<Destinatarios> query = null;

                Database context = new Database();

                query = from b in context.Destinatarios
                        where b.NombreDeUsuario.Contains(param)
                        && b.IdSector == idSector
                        select b;

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Result = new DestinatariosResponse() { ListaDestinatarios = DestinatariosResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<DestinatariosResponse> GetComboDestinatariosByDireccion(string param, string idDireccion)
        {
            try
            {
                IQueryable<Destinatarios> query = null;

                Database context = new Database();

                query = from b in context.Destinatarios
                        where b.NombreDeUsuario.Contains(param)
                        && b.IdBandeja == idDireccion
                        select b;

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Result = new DestinatariosResponse() { ListaDestinatarios = DestinatariosResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<DestinatariosResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<SectoresResponse> GetComboSectoresByDireccion(string param, string idDireccion)
        {
            try
            {
                IQueryable<Sectores> query = null;

                Database context = new Database();
                
                query = from b in context.Destinatarios
                        where b.Sectores.Descripcion.Contains(param)
                        && b.IdBandeja == idDireccion
                        select b.Sectores;
                
                return new GenericResponse<SectoresResponse>() { Code = 200, Result = new SectoresResponse() { ListaSectores = SectoresResponse.MapList(query.Take(100).Distinct().ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<SectoresResponse>() { Code = 200, Error = ex.Message };
            }

        }

        public GenericResponse<DireccionesResponse> GetComboDireccionesBySector(string param, string idSector)
        {
            try
            {
                IQueryable<Direcciones> query = null;

                Database context = new Database();

                query = from b in context.Destinatarios
                        where b.Direcciones.Descripcion.Contains(param)
                        && b.IdSector == idSector
                        select b.Direcciones;
                
                return new GenericResponse<DireccionesResponse>() { Code = 200, Result = new DireccionesResponse() { ListaDirecciones = DireccionesResponse.MapList(query.Take(100).Distinct().ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DESTINATARIOS, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<DireccionesResponse>() { Code = 200, Error = ex.Message };
            }

        }
    }
}
