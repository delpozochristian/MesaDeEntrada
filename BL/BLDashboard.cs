using System;
using System.Linq;
using DB.ViewModels;
using DB;

namespace BL
{
    public class BLDashboard
    {

        public GenericResponse<DashboardResponse> ObtenerDashboard()
        {
            GenericResponse<DashboardResponse> response = new GenericResponse<DashboardResponse>();
            response.Code = 200;

            try
            {
                Database db = new Database();

                int cantidadRecepciones = db.Pedidos.Where(x => x.Estado == Constants.Pedidos.ID_INGRESADO).Count();

                int cantidadPaquetes = db.Pedidos.Where(x => x.IdTipoDeProducto == 2).Count();

                int cantidadCorrespondencia = db.Pedidos.Where(x => x.Canalizaciones.Id == "1").Count();
                
                int cantidadEgresos = db.Pedidos.Where(x => x.Estado == Constants.Pedidos.ID_RETIRADO).Count();

                response.Result = new DashboardResponse();
                response.Result.CantidadRecepciones = cantidadRecepciones;
                response.Result.CantidadCorrespondencia = cantidadCorrespondencia;
                response.Result.CantidadEgresos = cantidadEgresos;
                response.Result.CantidadPaquetes = cantidadPaquetes;

                return response;
            }
            catch(Exception ex)
            {
                response.Code = 500;
                response.Error = "Error al obtener el dashboard";

                var message = ex.Message;
                var messageInner = ex.InnerException != null ? ex.InnerException.Message : "";

                DB.Database db2 = new DB.Database();
                db2.Log.Add(new DB.Log() { Fecha = DateTime.Now, Ubicacion = Constants.LOG_UBICACION_DASHBOARD, Mensaje = message, Detalle = messageInner });
                
                db2.SaveChanges();
            }

            return response;
        }
    }
}
