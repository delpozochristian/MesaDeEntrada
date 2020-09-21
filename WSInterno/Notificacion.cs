using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WSInterno
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface Notificacion
    {
        
        [OperationContract]
        string NotificarPedido(Pedido pedido);

        [OperationContract]
        string NotificarEstado(int estado, string codigoBarra);

        [OperationContract]
        string NotificarNuevoEstado(int estado, string codigoBarra, string email = "usuario@servicio", string fecha = null);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Pedido
    {
        [DataMember]
        public string NroSeguimiento { get; set; }

        [DataMember]
        public string AutorizadoRetirar { get; set; }

        [DataMember]
        public string NroDeOrdenCompra { get; set; }

        [DataMember]
        public string CodigoBarra { get; set; }

        [DataMember]
        public string Fecha { get; set; }

        [DataMember]
        public string TipoProducto { get; set; }

        [DataMember]
        public ImponerProveedorWSRequest Proveedor { get; set; }

        [DataMember]
        public ImponerRemitenteWSRequest Remitente { get; set; }

        [DataMember]
        public ImponerDestinatarioWSRequest Destinatario { get; set; }
        
        [DataMember]
        public string Observacion { get; set; }

        private PropertyInfo[] _PropertyInfos = null;

        public override string ToString()
        {
            if (_PropertyInfos == null)
                _PropertyInfos = this.GetType().GetProperties();

            var sb = new StringBuilder();

            foreach (var info in _PropertyInfos)
            {
                var value = info.GetValue(this, null) ?? "(null)";
                sb.AppendLine(info.Name + ": " + value.ToString());
            }

            return sb.ToString();
        }

    }

    [DataContract]
    public class ImponerProveedorWSRequest
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string RazonSocial { get; set; }

        [DataMember]
        public string Cuit { get; set; }

        [DataMember]
        public string NroRemito { get; set; }

        [DataMember]
        public string NroOrdenCompra { get; set; }
    }

    [DataContract]
    public class ImponerRemitenteWSRequest
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Sucursal { get; set; }

        [DataMember]
        public string Bandeja { get; set; }

        [DataMember]
        public string Sector { get; set; }
    }

    [DataContract]
    public class ImponerDestinatarioWSRequest
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Sucursal { get; set; }

        [DataMember]
        public string Bandeja { get; set; }

        [DataMember]
        public string Sector { get; set; }
        
        [DataMember]
        public string CentroDeCostos { get; set; }

        [DataMember]
        public string DescripcionCentroCostos { get; set; }

        [DataMember]
        public string DescripcionSector { get; set; }

        [DataMember]
        public string DescripcionBandeja { get; set; }

        [DataMember]
        public string Canalizacion { get; set; }
    }
}
