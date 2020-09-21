namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Archivos
    {
        public int Id { get; set; }

        public byte[] ArchivoRecepcion { get; set; }

        public int? IdIngreso { get; set; }

        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual Pedidos Pedidos { get; set; }
    }
}
