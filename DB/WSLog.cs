namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WSLog")]
    public partial class WSLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Servicio { get; set; }

        [Required]
        [StringLength(100)]
        public string Url { get; set; }

        [Required]
        public string JSONRequest { get; set; }

        [Required]
        public string JSONResponse { get; set; }

        public DateTime FechaLlamada { get; set; }

        public DateTime FechaRespuesta { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }
    }
}
