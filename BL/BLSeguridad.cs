using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB.ViewModels;
using DB;

namespace BL
{
    public class BLSeguridad
    {
        public GenericResponse<LoginResponse> CheckUserPass(string username, string password)
        {
            try
            {
                Database context = new Database();

                var query = from b in context.Usuarios
                            where b.email == username
                            && b.password == password
                            select b;

                if (query.Count() > 0)
                    return new GenericResponse<LoginResponse>() { Code = 200, Result = new LoginResponse() { Response = "Login Correcto.", Estado = true, username = username, password = password, IdUsuario = query.First().id } };

                return new GenericResponse<LoginResponse>() { Code = 200, Result = new LoginResponse() { Response = "Login Incorrecto.", Estado = false } };
            }
            catch(Exception ex)
            {
                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_RECEPCION, Mensaje = message, Detalle = messageInner });

                db2.SaveChanges();

                return new GenericResponse<LoginResponse>() { Code = 200, Result = new LoginResponse() { Response = "Login Incorrecto.", Estado = false } };
            }

            
        }
    }
}
