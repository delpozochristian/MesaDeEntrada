using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class SectoresResponse
    {
        public List<SectorResponse> ListaSectores { get; set; }

        public static SectorResponse Map(Sectores item)
        {
            SectorResponse response = new SectorResponse();
            response.Id = item.Id;
            response.Descripcion = item.Descripcion;
            response.IdDireccion = "";
            return response;
        }

        public static List<SectorResponse> MapList(List<Sectores> list)
        {
            List<SectorResponse> response = new List<SectorResponse>();

            foreach (var item in list)
            {
                response.Add(SectoresResponse.Map(item));
            }

            return response;
        }
    }

    public class SectorResponse
    {
        public string Id { get; set; }

        public string Descripcion { get; set; }

        public string IdDestinatario { get; set; }

        public string IdDireccion { get; set; }
    }
}
