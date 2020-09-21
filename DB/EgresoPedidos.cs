namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class EgresoPedidos
    {
        public int Id { get; set; }

        public int IdEgreso { get; set; }

        public int IdPedido { get; set; }

        public virtual Engresos Engresos { get; set; }

        public virtual Pedidos Pedidos { get; set; }
    }
}
