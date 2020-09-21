namespace DB
{
    using System.Data.Entity;

    public partial class Database : DbContext
    {
        public Database()
            : base("name=Database")
        {
        }

        public virtual DbSet<Archivos> Archivos { get; set; }
        public virtual DbSet<Canalizaciones> Canalizaciones { get; set; }
        public virtual DbSet<Destinatarios> Destinatarios { get; set; }
        public virtual DbSet<Direcciones> Direcciones { get; set; }
        public virtual DbSet<DireccionSector> DireccionSector { get; set; }
        public virtual DbSet<EgresoPedidos> EgresoPedidos { get; set; }
        public virtual DbSet<Engresos> Engresos { get; set; }
        public virtual DbSet<Estados> Estados { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Pedidos> Pedidos { get; set; }
        public virtual DbSet<Proveedores> Proveedores { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sectores> Sectores { get; set; }
        public virtual DbSet<TipoDeProductos> TipoDeProductos { get; set; }
        public virtual DbSet<UsuarioRoles> UsuarioRoles { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<WSLog> WSLog { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Canalizaciones>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Canalizaciones>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Canalizaciones)
                .HasForeignKey(e => e.IdCanalizacion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Destinatarios>()
                .Property(e => e.IdSector)
                .IsUnicode(false);

            modelBuilder.Entity<Destinatarios>()
                .Property(e => e.IdBandeja)
                .IsUnicode(false);

            modelBuilder.Entity<Destinatarios>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Destinatarios)
                .HasForeignKey(e => e.IdDestinatario)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Direcciones>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Direcciones>()
                .HasMany(e => e.Destinatarios)
                .WithOptional(e => e.Direcciones)
                .HasForeignKey(e => e.IdBandeja);

            modelBuilder.Entity<Direcciones>()
                .HasMany(e => e.DireccionSector)
                .WithOptional(e => e.Direcciones)
                .HasForeignKey(e => e.IdBandeja);

            modelBuilder.Entity<Direcciones>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Direcciones)
                .HasForeignKey(e => e.IdDireccion)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DireccionSector>()
                .Property(e => e.IdBandeja)
                .IsUnicode(false);

            modelBuilder.Entity<DireccionSector>()
                .Property(e => e.IdSector)
                .IsUnicode(false);

            modelBuilder.Entity<Engresos>()
                .HasMany(e => e.EgresoPedidos)
                .WithRequired(e => e.Engresos)
                .HasForeignKey(e => e.IdEgreso)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Estados>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Estados)
                .HasForeignKey(e => e.Estado)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdSector)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdDireccion)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdCanalizacion)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdRemitente)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdSectorRemitente)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdDireccionRemitente)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.IdSucursalRemitente)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .Property(e => e.AutorizadoRetirar)
                .IsUnicode(false);

            modelBuilder.Entity<Pedidos>()
                .HasMany(e => e.Archivos)
                .WithOptional(e => e.Pedidos)
                .HasForeignKey(e => e.IdIngreso);

            modelBuilder.Entity<Pedidos>()
                .HasMany(e => e.EgresoPedidos)
                .WithRequired(e => e.Pedidos)
                .HasForeignKey(e => e.IdPedido)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Proveedores>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Proveedores)
                .HasForeignKey(e => e.IdProveedor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.UsuarioRoles)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.id_rol)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sectores>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<Sectores>()
                .HasMany(e => e.Destinatarios)
                .WithOptional(e => e.Sectores)
                .HasForeignKey(e => e.IdSector);

            modelBuilder.Entity<Sectores>()
                .HasMany(e => e.DireccionSector)
                .WithOptional(e => e.Sectores)
                .HasForeignKey(e => e.IdSector);

            modelBuilder.Entity<Sectores>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.Sectores)
                .HasForeignKey(e => e.IdSector)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoDeProductos>()
                .HasMany(e => e.Pedidos)
                .WithRequired(e => e.TipoDeProductos)
                .HasForeignKey(e => e.IdTipoDeProducto)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Pedidos)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CanceladoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(c => c.PedidosModificados)
                .WithOptional(c => c.UsuarioEstadoPor)
                .HasForeignKey(c => c.EstadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Pedidos1)
                .WithRequired(e => e.Usuarios1)
                .HasForeignKey(e => e.UsuarioCreador)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.UsuarioRoles)
                .WithRequired(e => e.Usuarios)
                .HasForeignKey(e => e.id_usuario)
                .WillCascadeOnDelete(false);
        }
    }
}
