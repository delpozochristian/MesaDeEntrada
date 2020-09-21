using System;
using System.Collections.Generic;

namespace DB.ViewModels
{
    public class ProveedoresResponse
    {
        public List<ProveedorResponse> ListaProveedores { get; set; }
        public string totalCount { get; set; }


        public static ProveedorResponse Map(Proveedores item)
        {
            ProveedorResponse response = new ProveedorResponse();
            response.Id = item.Id;
            response.RazonSocial = item.RazonSocial;
            response.Estado = item.Estado;
            response.FechaAlta = item.FechaAlta;
            response.FechaBaja = item.FechaBaja;
            response.FechaAltaString = item.FechaAlta.HasValue ? item.FechaAlta.Value.ToString("dd/MM/yyyy") : "";
            response.FechaBajaString = item.FechaBaja.HasValue ? item.FechaBaja.Value.ToString("dd/MM/yyyy") : "";
            response.Cuit = item.Cuit;

            return response;
        }

        public static List<ProveedorResponse> MapList(List<Proveedores> list)
        {
            List<ProveedorResponse> response = new List<ProveedorResponse>();

            foreach (var item in list)
            {
                response.Add(ProveedoresResponse.Map(item));
            }

            return response;
        }
    }

    public class ProveedorResponse
    {
        public int Id { get; set; }

        public string Cuit { get; set; }

        public string RazonSocial { get; set; }

        public bool? Estado { get; set; }

        public DateTime? FechaAlta { get; set; }

        public DateTime? FechaBaja { get; set; }

        public string FechaAltaString { get; set; }

        public string FechaBajaString { get; set; }
    }
}
