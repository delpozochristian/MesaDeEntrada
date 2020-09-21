namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DireccionSector")]
    public partial class DireccionSector
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string IdBandeja { get; set; }

        [StringLength(12)]
        public string IdSector { get; set; }

        public virtual Direcciones Direcciones { get; set; }

        public virtual Sectores Sectores { get; set; }
    }
}
