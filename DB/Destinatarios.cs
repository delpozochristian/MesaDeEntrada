namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Destinatarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Destinatarios()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        [StringLength(100)]
        public string Id { get; set; }

        [StringLength(100)]
        public string NombreDeUsuario { get; set; }

        [StringLength(100)]
        public string IdSucursal { get; set; }

        [StringLength(100)]
        public string DescripcionSucursal { get; set; }

        [StringLength(12)]
        public string IdSector { get; set; }

        [StringLength(100)]
        public string DescripcionSector { get; set; }

        [StringLength(40)]
        public string IdBandeja { get; set; }

        [StringLength(100)]
        public string DescripcionBandeja { get; set; }

        [StringLength(100)]
        public string IdCentroDeCostos { get; set; }

        [StringLength(100)]
        public string DescripcionCentroDeCostos { get; set; }

        public virtual Direcciones Direcciones { get; set; }

        public virtual Sectores Sectores { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}
