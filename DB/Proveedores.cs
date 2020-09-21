namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Proveedores
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proveedores()
        {
            Pedidos = new HashSet<Pedidos>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Cuit { get; set; }

        [StringLength(50)]
        public string RazonSocial { get; set; }

        public bool? Estado { get; set; }

        public DateTime? FechaAlta { get; set; }

        public DateTime? FechaBaja { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pedidos> Pedidos { get; set; }
    }
}
