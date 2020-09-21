using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class GuardarRecepcionRequest
    {
        //General
        public string NroSeguimiento { get; set; }
        public bool EscaneoSeguridad { get; set; }
        public bool ImprimirEtiqueta { get; set; }
        public Canalizaciones Canalizacion { get; set; }
        public int IdArchivo { get; set; }
        public int IdTipoProducto { get; set; }

        //Proveedor
        public int IdProveedor { get; set; }
        public string NroRemitoProveedor { get; set; }
        public string NroOrdenCompraProveedor { get; set; }
        public string RazonSocialProveedor { get; set; }
        public string CuitProveedor { get; set; }

        //Destinatario
        public string IdDestinatario { get; set; }
        public string IdSectorDestinatario { get; set; }
        public string IdDireccionDestinatario { get; set; }
        public string IdSucursalDestinatario { get; set; }

        //Remitente
        public string IdRemitente { get; set; }
        public string IdSectorRemitente { get; set; }
        public string IdDireccionRemitente { get; set; }
        public string IdSucursalRemitente { get; set; }

        //Extra
        public string Observacion { get; set; }
        public int IdUsuario { get; set; }
    }
}
