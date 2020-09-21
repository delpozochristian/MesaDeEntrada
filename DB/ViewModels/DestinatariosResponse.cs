using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class DestinatariosResponse
    {
        public List<DestinatarioResponse> ListaDestinatarios { get; set; }

        public static DestinatarioResponse Map(Destinatarios item)
        {
            DestinatarioResponse response = new DestinatarioResponse();
            response.Id = item.Id;
            response.NombreDeUsuario = item.NombreDeUsuario;
            response.IdSucursal = item.IdSucursal;
            response.DescripcionSucursal = item.DescripcionSucursal;
            response.IdSector = item.IdSector;
            response.DescripcionSector = item.DescripcionSector;
            response.IdBandeja = item.IdBandeja;
            response.DescripcionBandeja = item.DescripcionBandeja;
            response.IdCentroDeCostos = item.IdCentroDeCostos;
            response.DescripcionCentroDeCostos = item.DescripcionCentroDeCostos;

            return response;
        }

        public static List<DestinatarioResponse> MapList(List<Destinatarios> list)
        {
            List<DestinatarioResponse> response = new List<DestinatarioResponse>();

            foreach (var item in list)
            {
                response.Add(DestinatariosResponse.Map(item));
            }

            return response;
        }
    }

    public class DestinatarioResponse
    {
        public string Id { get; set; }
        
        public string NombreDeUsuario { get; set; }
        
        public string IdSucursal { get; set; }
        
        public string DescripcionSucursal { get; set; }
        
        public string IdSector { get; set; }
        
        public string DescripcionSector { get; set; }
        
        public string IdBandeja { get; set; }
        
        public string DescripcionBandeja { get; set; }
        
        public string IdCentroDeCostos { get; set; }
        
        public string DescripcionCentroDeCostos { get; set; }

    }
}
