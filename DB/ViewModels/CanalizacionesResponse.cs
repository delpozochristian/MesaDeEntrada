using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class CanalizacionesResponse
    {
        public List<CanalizacionResponse> ListaCanalizaciones { get; set; }

        public static CanalizacionResponse Map(Canalizaciones item)
        {
            CanalizacionResponse response = new CanalizacionResponse();
            response.Id = item.Id;
            response.Descripcion = item.Descripcion;
            return response;
        }

        public static List<CanalizacionResponse> MapList(List<Canalizaciones> list)
        {
            List<CanalizacionResponse> response = new List<CanalizacionResponse>();

            foreach (var item in list)
            {
                response.Add(CanalizacionesResponse.Map(item));
            }

            return response;
        }
    }

    public class CanalizacionResponse
    {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public string IdDireccion { get; set; }
    }
}
