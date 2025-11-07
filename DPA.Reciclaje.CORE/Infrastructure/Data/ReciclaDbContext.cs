using System;
using System.Collections.Generic;
using DPA.Reciclaje.CORE.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Data;

public partial class ReciclaDbContext : DbContext
{
    public ReciclaDbContext()
    {
    }

    public ReciclaDbContext(DbContextOptions<ReciclaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campaña> Campaña { get; set; }

    public virtual DbSet<CampañaImg> CampañaImg { get; set; }

    public virtual DbSet<Carrito> Carrito { get; set; }

    public virtual DbSet<CarritoProducto> CarritoProducto { get; set; }

    public virtual DbSet<Categoría> Categoría { get; set; }

    public virtual DbSet<Chat> Chat { get; set; }

    public virtual DbSet<ChatMensaje> ChatMensaje { get; set; }

    public virtual DbSet<Comentario> Comentario { get; set; }

    public virtual DbSet<ComentarioImg> ComentarioImg { get; set; }

    public virtual DbSet<Departamento> Departamento { get; set; }

    public virtual DbSet<Distrito> Distrito { get; set; }

    public virtual DbSet<Favorito> Favorito { get; set; }

    public virtual DbSet<Interaccion> Interaccion { get; set; }

    public virtual DbSet<MetodoPago> MetodoPago { get; set; }

    public virtual DbSet<Notificacion> Notificacion { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Pais> Pais { get; set; }

    public virtual DbSet<Producto> Producto { get; set; }

    public virtual DbSet<ProductoImg> ProductoImg { get; set; }

    public virtual DbSet<Provincia> Provincia { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ReciclaDB;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaña>(entity =>
        {
            entity.HasKey(e => e.IdCampaña).HasName("PK__Campaña__16852050FC9341C7");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Título)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Campaña)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("FK__Campaña__IdDistr__5BE2A6F2");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Campaña)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Campaña__IdUsuar__5CD6CB2B");
        });

        modelBuilder.Entity<CampañaImg>(entity =>
        {
            entity.HasKey(e => e.IdCampañaImg).HasName("PK__CampañaI__C15CCC29B72B589F");

            entity.HasOne(d => d.IdCampañaNavigation).WithMany(p => p.CampañaImg)
                .HasForeignKey(d => d.IdCampaña)
                .HasConstraintName("FK__CampañaIm__IdCam__5FB337D6");
        });

        modelBuilder.Entity<Carrito>(entity =>
        {
            entity.HasKey(e => e.IdCarrito).HasName("PK__Carrito__8B4A618CC9D51721");

            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Carrito)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Carrito__IdUsuar__6754599E");
        });

        modelBuilder.Entity<CarritoProducto>(entity =>
        {
            entity.HasKey(e => e.IdCarritoItem).HasName("PK__CarritoP__7C333CD3814538F8");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.CarritoProducto)
                .HasForeignKey(d => d.IdCarrito)
                .HasConstraintName("FK__CarritoPr__IdCar__6B24EA82");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CarritoProducto)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__CarritoPr__IdPro__6C190EBB");
        });

        modelBuilder.Entity<Categoría>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__Categorí__A3C02A10FB28E320");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.IdChat).HasName("PK__Chat__3817F38C70CDC080");

            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaInicio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Chat)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Chat__IdProducto__534D60F1");

            entity.HasOne(d => d.IdUsuarioCompradorNavigation).WithMany(p => p.ChatIdUsuarioCompradorNavigation)
                .HasForeignKey(d => d.IdUsuarioComprador)
                .HasConstraintName("FK__Chat__IdUsuarioC__52593CB8");

            entity.HasOne(d => d.IdUsuarioVendedorNavigation).WithMany(p => p.ChatIdUsuarioVendedorNavigation)
                .HasForeignKey(d => d.IdUsuarioVendedor)
                .HasConstraintName("FK__Chat__IdUsuarioV__5165187F");
        });

        modelBuilder.Entity<ChatMensaje>(entity =>
        {
            entity.HasKey(e => e.IdChatMensaje).HasName("PK__ChatMens__CE49C95ED1881F13");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Leído).HasDefaultValue(false);
            entity.Property(e => e.Texto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdChatNavigation).WithMany(p => p.ChatMensaje)
                .HasForeignKey(d => d.IdChat)
                .HasConstraintName("FK__ChatMensa__IdCha__571DF1D5");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdComentario).HasName("PK__Comentar__DDBEFBF98FCDBD1B");

            entity.Property(e => e.Calificacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Texto)
                .HasMaxLength(1000)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Comentario)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Comentari__IdPro__4AB81AF0");

            entity.HasOne(d => d.IdUsuarioCompradorNavigation).WithMany(p => p.ComentarioIdUsuarioCompradorNavigation)
                .HasForeignKey(d => d.IdUsuarioComprador)
                .HasConstraintName("FK__Comentari__IdUsu__49C3F6B7");

            entity.HasOne(d => d.IdUsuarioVendedorNavigation).WithMany(p => p.ComentarioIdUsuarioVendedorNavigation)
                .HasForeignKey(d => d.IdUsuarioVendedor)
                .HasConstraintName("FK__Comentari__IdUsu__48CFD27E");
        });

        modelBuilder.Entity<ComentarioImg>(entity =>
        {
            entity.HasKey(e => e.IdComentarioImg).HasName("PK__Comentar__4EC5F1C5FBA9B17A");

            entity.HasOne(d => d.IdComentarioNavigation).WithMany(p => p.ComentarioImg)
                .HasForeignKey(d => d.IdComentario)
                .HasConstraintName("FK__Comentari__IdCom__4E88ABD4");
        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D4F6BE497");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Departamento)
                .HasForeignKey(d => d.IdPais)
                .HasConstraintName("FK__Departame__IdPai__25869641");
        });

        modelBuilder.Entity<Distrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PK__Distrito__DE8EED5917EDDEBD");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Distrito)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Distrito__IdDepa__2B3F6F97");

            entity.HasOne(d => d.IdProvinciaNavigation).WithMany(p => p.Distrito)
                .HasForeignKey(d => d.IdProvincia)
                .HasConstraintName("FK__Distrito__IdProv__2C3393D0");
        });

        modelBuilder.Entity<Favorito>(entity =>
        {
            entity.HasKey(e => e.IdFavorito).HasName("PK__Favorito__39DCEE50D157C758");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Favorito)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Favorito__IdProd__3E52440B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Favorito)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Favorito__IdUsua__3D5E1FD2");
        });

        modelBuilder.Entity<Interaccion>(entity =>
        {
            entity.HasKey(e => e.Idinteraccion).HasName("PK__Interacc__6A334ADAB680CE8F");

            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Operacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Interaccion)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Interacci__IdPro__6383C8BA");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Interaccion)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Interacci__IdUsu__628FA481");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BE961FC9B4");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Notificacion>(entity =>
        {
            entity.HasKey(e => e.IdNotificacion).HasName("PK__Notifica__F6CA0A85119D06FA");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Leído).HasDefaultValue(false);
            entity.Property(e => e.Mensaje)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Notificacion)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__Notificac__IdPro__440B1D61");

            entity.HasOne(d => d.IdUsuarioCompradorNavigation).WithMany(p => p.NotificacionIdUsuarioCompradorNavigation)
                .HasForeignKey(d => d.IdUsuarioComprador)
                .HasConstraintName("FK__Notificac__IdUsu__4316F928");

            entity.HasOne(d => d.IdUsuarioVendedorNavigation).WithMany(p => p.NotificacionIdUsuarioVendedorNavigation)
                .HasForeignKey(d => d.IdUsuarioVendedor)
                .HasConstraintName("FK__Notificac__IdUsu__4222D4EF");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PK__Pagos__FC851A3ABA87EECE");

            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Monto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroOperacion)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCarritoNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdCarrito)
                .HasConstraintName("FK__Pagos__IdCarrito__71D1E811");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdMetodoPago)
                .HasConstraintName("FK__Pagos__IdMetodoP__72C60C4A");
        });

        modelBuilder.Entity<Pais>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Pais__FC850A7B33FB19B6");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210A8F6C93F");

            entity.Property(e => e.Descripción)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Disponible)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaPublicación)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Motivo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__Producto__IdCate__35BCFE0A");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Producto)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Producto__IdUsua__37A5467C");
        });

        modelBuilder.Entity<ProductoImg>(entity =>
        {
            entity.HasKey(e => e.IdProductoImg).HasName("PK__Producto__3246508A48DDB811");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductoImg)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK__ProductoI__IdPro__3A81B327");
        });

        modelBuilder.Entity<Provincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PK__Provinci__EED74455CC9084A0");

            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Provincia)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Provincia__IdDep__286302EC");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97D998D525");

            entity.HasIndex(e => e.Email, "UQ__Usuario__A9D105340A87FE7D").IsUnique();

            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Puntuacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Rol)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Situacion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UltAccionCompra)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UltAccionVenta)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.IdDistritoNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdDistrito)
                .HasConstraintName("FK__Usuario__IdDistr__300424B4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
