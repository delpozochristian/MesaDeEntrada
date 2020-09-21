namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Ubicacion { get; set; }

        [Required]
        [StringLength(500)]
        public string Mensaje { get; set; }

        public string Detalle { get; set; }

        public DateTime Fecha { get; set; }
    }
}
