using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class DireccionesResponse
    {
        public List<DireccionResponse> ListaDirecciones { get; set; }

        public static DireccionResponse Map(Direcciones item)
        {
            DireccionResponse response = new DireccionResponse();
            response.Id = item.Id;
            response.Descripcion = item.Descripcion;
            response.IdSector = "";

            return response;
        }

        public static List<DireccionResponse> MapList(List<Direcciones> list)
        {
            List<DireccionResponse> response = new List<DireccionResponse>();

            foreach (var item in list)
            {
                response.Add(DireccionesResponse.Map(item));
            }

            return response;
        }
    }

    public class DireccionResponse
    {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public string IdSector { get; set; }
    }
}
