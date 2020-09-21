namespace DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsuarioRoles
    {
        public int id { get; set; }

        public int id_usuario { get; set; }

        public int id_rol { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
