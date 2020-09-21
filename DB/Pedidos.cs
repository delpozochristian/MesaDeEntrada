namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Pedidos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pedidos()
        {
            Archivos = new HashSet<Archivos>();
            EgresoPedidos = new HashSet<EgresoPedidos>();
        }

        public int Id { get; set; }

        public int Estado { get; set; }

        [StringLength(50)]
        public string NroSeguimiento { get; set; }

        public int IdProveedor { get; set; }

        [Required]
        [StringLength(100)]
        public string IdDestinatario { get; set; }

        [Required]
        [StringLength(12)]
        public string IdSector { get; set; }

        [Required]
        [StringLength(40)]
        public string IdDireccion { get; set; }

        [Required]
        [StringLength(30)]
        public string IdCanalizacion { get; set; }

        public int IdTipoDeProducto { get; set; }

        [StringLength(100)]
        public string NroRemito { get; set; }

        [StringLength(100)]
        public string NroOrdeDeCompra { get; set; }

        [StringLength(50)]
        public string CodigoBarra { get; set; }

        public bool? EscaneoDePieza { get; set; }

        public bool? ImprimirEtiqueta { get; set; }

        [StringLength(500)]
        public string Observacion { get; set; }

        public int UsuarioCreador { get; set; }

        public DateTime? FechaImposicion { get; set; }

        [Required]
        [StringLength(30)]
        public string IdRemitente { get; set; }

        [Required]
        [StringLength(30)]
        public string IdSectorRemitente { get; set; }

        [Required]
        [StringLength(40)]
        public string IdDireccionRemitente { get; set; }

        [Required]
        [StringLength(30)]
        public string IdSucursalRemitente { get; set; }

        public int? CanceladoPor { get; set; }

        public DateTime? FechaCancelacion { get; set; }

        public int? EstadoPor { get; set; }
        public virtual Usuarios UsuarioEstadoPor { get; set; }

        public DateTime? EstadoFecha { get; set; }

        [StringLength(50)]
        public string AutorizadoRetirar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Archivos> Archivos { get; set; }

        public virtual Canalizaciones Canalizaciones { get; set; }

        public virtual Destinatarios Destinatarios { get; set; }

        public virtual Direcciones Direcciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EgresoPedidos> EgresoPedidos { get; set; }

        public virtual Estados Estados { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Proveedores Proveedores { get; set; }

        public virtual Sectores Sectores { get; set; }

        public virtual TipoDeProductos TipoDeProductos { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }
    }
}
