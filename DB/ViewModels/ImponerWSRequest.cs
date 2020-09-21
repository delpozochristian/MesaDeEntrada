using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.ViewModels
{
    public class ImponerWSRequest
    {
        //string nroSeguimiento, string fecha, string tipoProducto, string idProveedor,string razonSocial, string cuit, string nroRemito, string ordenCompra, 
        //string idUsuarioRemitente,string sucursal, string bandeja, string sector, string idDestinatario, string observacion

        public string NroSeguimiento { get; set; }
        public string Fecha { get; set; }
        public string TipoProducto { get; set; }
        public ImponerProveedorWSRequest Proveedor { get; set; }
        public ImponerRemitenteWSRequest Remitente { get; set; }
        public ImponerDestinatarioWSRequest Destinatario { get; set; }

        public class ImponerProveedorWSRequest
        {
            public string Id { get; set; }
            public string RazonSocial { get; set; }
            public string Cuit { get; set; }
            public string NroRemito { get; set; }
            public string NroOrdenCompra { get; set; }
        }

        public class ImponerRemitenteWSRequest
        {
            public string Id { get; set; }
            public string Sucursal { get; set; }
            public string Bandeja { get; set; }
            public string Sector { get; set; }
        }

        public class ImponerDestinatarioWSRequest
        {
            public string Id { get; set; }
            public string Sucursal { get; set; }
            public string Bandeja { get; set; }
            public string Sector { get; set; }
        }

        public string Observacion { get; set; }
        public string Canalizacion { get; set; }
    }
}
