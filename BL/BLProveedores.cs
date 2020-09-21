using System;
using System.Linq;
using DB.ViewModels;
using DB;
using MvcPaging;
using System.Globalization;

namespace BL
{
    public class BLProveedores
    {
        

        public GenericResponse<ProveedoresResponse> GetCombo(string param)
        {
            try
            {
                Database context = new Database();

                if (String.IsNullOrEmpty(param))
                {
                    var query2 = from b in context.Proveedores
                                 where b.Estado == true
                                 select b;

                    return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { ListaProveedores = ProveedoresResponse.MapList(query2.ToList()) } };
                }

                var query = from b in context.Proveedores
                            where b.RazonSocial.Contains(param)
                            && b.Estado == true
                            select b;

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { ListaProveedores = ProveedoresResponse.MapList(query.ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Error = ex.Message };
            }
        }

        private bool CUITValido(string _CUIT)
        {
            if (_CUIT.Length == 0) return true;
            string CUITValidado = string.Empty;
            bool Valido = false;
            char Ch;
            for (int i = 0; i < _CUIT.Length; i++)
            {
                Ch = _CUIT[i];
                if ((Ch > 47) && (Ch < 58))
                {
                    CUITValidado = CUITValidado + Ch;
                }
            }

            _CUIT = CUITValidado;
            Valido = (_CUIT.Length == 11);
            if (Valido)
            {
                int Verificador = EncontrarVerificador(_CUIT);
                Valido = (_CUIT[10].ToString() == Verificador.ToString());
            }

            return Valido;
        }

        private int EncontrarVerificador(string CUIT)
        {
            int Sumador = 0;
            int Producto = 0;
            int Coeficiente = 0;
            int Resta = 5;
            for (int i = 0; i < 10; i++)
            {
                if (i == 4) Resta = 11;
                Producto = CUIT[i];
                Producto -= 48;
                Coeficiente = Resta - i;
                Producto = Producto * Coeficiente;
                Sumador = Sumador + Producto;
            }

            int Resultado = Sumador - (11 * (Sumador / 11));
            Resultado = 11 - Resultado;

            if (Resultado == 11) return 0;
            else return Resultado;
        }

        public GenericResponse<ProveedoresResponse> GetComboReportes(string param)
        {
            try
            {
                Database context = new Database();

                if (String.IsNullOrEmpty(param))
                {
                    var query2 = from b in context.Proveedores
                                 select b;

                    return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { ListaProveedores = ProveedoresResponse.MapList(query2.ToList()) } };
                }

                var query = from b in context.Proveedores
                            where b.RazonSocial.Contains(param)
                            && b.Estado == true
                            select b;

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { ListaProveedores = ProveedoresResponse.MapList(query.Take(100).ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Error = ex.Message };
            }
        }

        public GenericResponse<ProveedoresResponse> GetAll(int pageIndex, int pageSize, string razonSocial, string cuit, string estado, string fechaDesde, string fechaHasta)
        {
            try
            {
                Database context = new Database();

                var query2 = from b in context.Proveedores.OrderByDescending(x => x.FechaAlta)
                             select b;

                if (!string.IsNullOrWhiteSpace(razonSocial))
                {
                    query2 = from b in query2 where b.RazonSocial.Contains(razonSocial) select b;
                }

                if (!string.IsNullOrWhiteSpace(cuit))
                {
                    query2 = from b in query2 where b.Cuit == cuit select b;
                }

                if (!string.IsNullOrEmpty(estado))
                {
                    switch (estado)
                    {
                        case "Habilitados":
                            query2 = query2.Where(w => w.Estado.HasValue && w.Estado.Value);
                            break;
                        case "Deshabilitados":
                            query2 = query2.Where(w => w.Estado.HasValue && !w.Estado.Value);
                            break;
                        default:
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(fechaDesde))
                {
                    CultureInfo esAR = new CultureInfo("es-AR");
                    DateTime fechaSeleccioandaDesde = DateTime.ParseExact(fechaDesde + " 00:00", "dd/MM/yyyy HH:mm", esAR);
                    query2 = query2.Where(w => w.FechaAlta.Value > fechaSeleccioandaDesde);
                }


                if (!string.IsNullOrWhiteSpace(fechaHasta))
                {
                    CultureInfo esAR = new CultureInfo("es-AR");
                    DateTime fechaSeleccioandaHasta = DateTime.ParseExact(fechaHasta + " 23:59", "dd/MM/yyyy HH:mm", esAR);
                    query2 = query2.Where(w => w.FechaAlta.Value < fechaSeleccioandaHasta);
                }

                var listaProveedores = query2.ToPagedList(pageIndex - 1, pageSize);
                return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { totalCount = listaProveedores.TotalItemCount.ToString(), ListaProveedores = ProveedoresResponse.MapList(listaProveedores.ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Error = ex.Message };
            }
        }

        public GenericResponse<ProveedoresResponse> Get(int id)
        {
            try
            {
                Database context = new Database();

                var query2 = from b in context.Proveedores
                             where b.Id == id
                             select b;

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Result = new ProveedoresResponse() { ListaProveedores = ProveedoresResponse.MapList(query2.ToList()) } };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<ProveedoresResponse>() { Code = 200, Error = ex.Message };
            }
        }

        public GenericResponse<bool> Save(int? Id, string RazonSocial, string Cuit)
        {
            try
            {
                Database context = new Database();

                Proveedores proveedor = new Proveedores();
                Proveedores proveedorCuit = new Proveedores();

                if (Id.HasValue)
                    proveedor = context.Proveedores.Where(x => x.Id == Id.Value).FirstOrDefault();

                var messageError = "";

                if (String.IsNullOrEmpty(RazonSocial) && !String.IsNullOrEmpty(Cuit))
                    messageError += "La Razón Social es obligatoria.";
                else if (String.IsNullOrEmpty(RazonSocial) && String.IsNullOrEmpty(Cuit))
                    messageError += "La Razón Social y el Cuit son obligatorios.";
                else if (!String.IsNullOrEmpty(RazonSocial) && String.IsNullOrEmpty(Cuit))
                    messageError += "La CUIT es obligatoria.";
                else if (!CUITValido(Cuit))
                    messageError += "La CUIT no tiene un formato válido.";

                if (Id.HasValue)
                {
                    proveedorCuit = context.Proveedores.Where(x => x.Cuit == Cuit && x.Id != Id).FirstOrDefault();
                }
                else
                {
                    proveedorCuit = context.Proveedores.Where(x => x.Cuit == Cuit).FirstOrDefault();
                }

                if (proveedorCuit != null)
                    messageError += "Ya existe registrado un proveedor con la CUIT: " + Cuit + ".";


                if (messageError.Length > 0)
                    return new GenericResponse<bool>() { Code = 501, Error = messageError };

                proveedor.RazonSocial = RazonSocial;
                proveedor.Cuit = Cuit;
                proveedor.FechaAlta = DateTime.Now;
                proveedor.Estado = true;

                if (!Id.HasValue)
                    context.Proveedores.Add(proveedor);

                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

        public GenericResponse<bool> Delete(int id)
        {
            try
            {
                Database context = new Database();

                Proveedores proveedor = context.Proveedores.Where(x => x.Id == id).FirstOrDefault();
                proveedor.Estado = false;
                proveedor.FechaBaja = DateTime.Now;
                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

        public GenericResponse<bool> Enable(int id)
        {
            try
            {
                Database context = new Database();

                Proveedores proveedor = context.Proveedores.Where(x => x.Id == id).FirstOrDefault();
                proveedor.Estado = true;
                proveedor.FechaBaja = null;
                context.SaveChanges();

                return new GenericResponse<bool>() { Code = 200, Result = true };
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_PROVEEDORES, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<bool>() { Code = 500, Error = ex.Message };
            }
        }

    }
}
