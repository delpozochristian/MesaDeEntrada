using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class EstadosResponse
    {
        public List<EstadoResponse> ListaEstados { get; set; }

        public static EstadoResponse Map(Estados item)
        {
            EstadoResponse response = new EstadoResponse();
            response.Id = item.Id;
            response.Nombre = item.Nombre;
            return response;
        }

        public static List<EstadoResponse> MapList(List<Estados> list)
        {
            List<EstadoResponse> response = new List<EstadoResponse>();

            foreach (var item in list)
            {
                response.Add(EstadosResponse.Map(item));
            }

            return response;
        }
    }

    public class EstadoResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
    }
}
