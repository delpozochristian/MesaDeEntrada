﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSExterno.NotificarPedido {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Pedido", Namespace="http://schemas.datacontract.org/2004/07/WSInterno")]
    [System.SerializableAttribute()]
    public partial class Pedido : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WSExterno.NotificarPedido.ImponerDestinatarioWSRequest DestinatarioField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FechaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NroDeOrdenCompraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NroSeguimientoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ObservacionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WSExterno.NotificarPedido.ImponerProveedorWSRequest ProveedorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private WSExterno.NotificarPedido.ImponerRemitenteWSRequest RemitenteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TipoProductoField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WSExterno.NotificarPedido.ImponerDestinatarioWSRequest Destinatario {
            get {
                return this.DestinatarioField;
            }
            set {
                if ((object.ReferenceEquals(this.DestinatarioField, value) != true)) {
                    this.DestinatarioField = value;
                    this.RaisePropertyChanged("Destinatario");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Fecha {
            get {
                return this.FechaField;
            }
            set {
                if ((object.ReferenceEquals(this.FechaField, value) != true)) {
                    this.FechaField = value;
                    this.RaisePropertyChanged("Fecha");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NroDeOrdenCompra {
            get {
                return this.NroDeOrdenCompraField;
            }
            set {
                if ((object.ReferenceEquals(this.NroDeOrdenCompraField, value) != true)) {
                    this.NroDeOrdenCompraField = value;
                    this.RaisePropertyChanged("NroDeOrdenCompra");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NroSeguimiento {
            get {
                return this.NroSeguimientoField;
            }
            set {
                if ((object.ReferenceEquals(this.NroSeguimientoField, value) != true)) {
                    this.NroSeguimientoField = value;
                    this.RaisePropertyChanged("NroSeguimiento");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Observacion {
            get {
                return this.ObservacionField;
            }
            set {
                if ((object.ReferenceEquals(this.ObservacionField, value) != true)) {
                    this.ObservacionField = value;
                    this.RaisePropertyChanged("Observacion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WSExterno.NotificarPedido.ImponerProveedorWSRequest Proveedor {
            get {
                return this.ProveedorField;
            }
            set {
                if ((object.ReferenceEquals(this.ProveedorField, value) != true)) {
                    this.ProveedorField = value;
                    this.RaisePropertyChanged("Proveedor");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public WSExterno.NotificarPedido.ImponerRemitenteWSRequest Remitente {
            get {
                return this.RemitenteField;
            }
            set {
                if ((object.ReferenceEquals(this.RemitenteField, value) != true)) {
                    this.RemitenteField = value;
                    this.RaisePropertyChanged("Remitente");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TipoProducto {
            get {
                return this.TipoProductoField;
            }
            set {
                if ((object.ReferenceEquals(this.TipoProductoField, value) != true)) {
                    this.TipoProductoField = value;
                    this.RaisePropertyChanged("TipoProducto");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ImponerDestinatarioWSRequest", Namespace="http://schemas.datacontract.org/2004/07/WSInterno")]
    [System.SerializableAttribute()]
    public partial class ImponerDestinatarioWSRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BandejaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CanalizacionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CentroDeCostosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionBandejaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionCentroCostosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionSectorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NombreField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SectorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SucursalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Bandeja {
            get {
                return this.BandejaField;
            }
            set {
                if ((object.ReferenceEquals(this.BandejaField, value) != true)) {
                    this.BandejaField = value;
                    this.RaisePropertyChanged("Bandeja");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Canalizacion {
            get {
                return this.CanalizacionField;
            }
            set {
                if ((object.ReferenceEquals(this.CanalizacionField, value) != true)) {
                    this.CanalizacionField = value;
                    this.RaisePropertyChanged("Canalizacion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string CentroDeCostos {
            get {
                return this.CentroDeCostosField;
            }
            set {
                if ((object.ReferenceEquals(this.CentroDeCostosField, value) != true)) {
                    this.CentroDeCostosField = value;
                    this.RaisePropertyChanged("CentroDeCostos");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescripcionBandeja {
            get {
                return this.DescripcionBandejaField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionBandejaField, value) != true)) {
                    this.DescripcionBandejaField = value;
                    this.RaisePropertyChanged("DescripcionBandeja");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescripcionCentroCostos {
            get {
                return this.DescripcionCentroCostosField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionCentroCostosField, value) != true)) {
                    this.DescripcionCentroCostosField = value;
                    this.RaisePropertyChanged("DescripcionCentroCostos");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DescripcionSector {
            get {
                return this.DescripcionSectorField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionSectorField, value) != true)) {
                    this.DescripcionSectorField = value;
                    this.RaisePropertyChanged("DescripcionSector");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nombre {
            get {
                return this.NombreField;
            }
            set {
                if ((object.ReferenceEquals(this.NombreField, value) != true)) {
                    this.NombreField = value;
                    this.RaisePropertyChanged("Nombre");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sector {
            get {
                return this.SectorField;
            }
            set {
                if ((object.ReferenceEquals(this.SectorField, value) != true)) {
                    this.SectorField = value;
                    this.RaisePropertyChanged("Sector");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sucursal {
            get {
                return this.SucursalField;
            }
            set {
                if ((object.ReferenceEquals(this.SucursalField, value) != true)) {
                    this.SucursalField = value;
                    this.RaisePropertyChanged("Sucursal");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ImponerProveedorWSRequest", Namespace="http://schemas.datacontract.org/2004/07/WSInterno")]
    [System.SerializableAttribute()]
    public partial class ImponerProveedorWSRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CuitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NroOrdenCompraField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NroRemitoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RazonSocialField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Cuit {
            get {
                return this.CuitField;
            }
            set {
                if ((object.ReferenceEquals(this.CuitField, value) != true)) {
                    this.CuitField = value;
                    this.RaisePropertyChanged("Cuit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NroOrdenCompra {
            get {
                return this.NroOrdenCompraField;
            }
            set {
                if ((object.ReferenceEquals(this.NroOrdenCompraField, value) != true)) {
                    this.NroOrdenCompraField = value;
                    this.RaisePropertyChanged("NroOrdenCompra");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NroRemito {
            get {
                return this.NroRemitoField;
            }
            set {
                if ((object.ReferenceEquals(this.NroRemitoField, value) != true)) {
                    this.NroRemitoField = value;
                    this.RaisePropertyChanged("NroRemito");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string RazonSocial {
            get {
                return this.RazonSocialField;
            }
            set {
                if ((object.ReferenceEquals(this.RazonSocialField, value) != true)) {
                    this.RazonSocialField = value;
                    this.RaisePropertyChanged("RazonSocial");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ImponerRemitenteWSRequest", Namespace="http://schemas.datacontract.org/2004/07/WSInterno")]
    [System.SerializableAttribute()]
    public partial class ImponerRemitenteWSRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BandejaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SectorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SucursalField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Bandeja {
            get {
                return this.BandejaField;
            }
            set {
                if ((object.ReferenceEquals(this.BandejaField, value) != true)) {
                    this.BandejaField = value;
                    this.RaisePropertyChanged("Bandeja");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sector {
            get {
                return this.SectorField;
            }
            set {
                if ((object.ReferenceEquals(this.SectorField, value) != true)) {
                    this.SectorField = value;
                    this.RaisePropertyChanged("Sector");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Sucursal {
            get {
                return this.SucursalField;
            }
            set {
                if ((object.ReferenceEquals(this.SucursalField, value) != true)) {
                    this.SucursalField = value;
                    this.RaisePropertyChanged("Sucursal");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="NotificarPedido.Notificacion")]
    public interface Notificacion {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Notificacion/NotificarPedido", ReplyAction="http://tempuri.org/Notificacion/NotificarPedidoResponse")]
        string NotificarPedido(WSExterno.NotificarPedido.Pedido pedido);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Notificacion/NotificarPedido", ReplyAction="http://tempuri.org/Notificacion/NotificarPedidoResponse")]
        System.Threading.Tasks.Task<string> NotificarPedidoAsync(WSExterno.NotificarPedido.Pedido pedido);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface NotificacionChannel : WSExterno.NotificarPedido.Notificacion, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NotificacionClient : System.ServiceModel.ClientBase<WSExterno.NotificarPedido.Notificacion>, WSExterno.NotificarPedido.Notificacion {
        
        public NotificacionClient() {
        }
        
        public NotificacionClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NotificacionClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotificacionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NotificacionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string NotificarPedido(WSExterno.NotificarPedido.Pedido pedido) {
            return base.Channel.NotificarPedido(pedido);
        }
        
        public System.Threading.Tasks.Task<string> NotificarPedidoAsync(WSExterno.NotificarPedido.Pedido pedido) {
            return base.Channel.NotificarPedidoAsync(pedido);
        }
    }
}
