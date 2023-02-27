using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace riesgos_backend.Models;

public partial class SoftbankContext : DbContext
{
    public SoftbankContext()
    {
    }

    public SoftbankContext(DbContextOptions<SoftbankContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Rol1> Rols1 { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Usuario1> Usuarios1 { get; set; }

    public virtual DbSet<UsuarioRiesgos> UsuarioRiesgos { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }

    public virtual DbSet<UsuarioRol1> UsuarioRols1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=192.168.3.5;Database=Softbank;user id=sa;password=Cloudred987;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_CS_CAT.ROL");

            entity.ToTable("ROL", "CS_CAT");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Rol1>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("ROL", "SEGURIDAD");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Nivel).HasColumnName("NIVEL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_CS_CAT.USUARIO");

            entity.ToTable("USUARIO", "CS_CAT");

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Cambioclaveproximoingreso).HasColumnName("CAMBIOCLAVEPROXIMOINGRESO");
            entity.Property(e => e.Clave)
                .HasMaxLength(250)
                .HasColumnName("CLAVE");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fechacreacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHACREACION");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(30)
                .HasColumnName("IDENTIFICACION");
            entity.Property(e => e.Interno).HasColumnName("INTERNO");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<Usuario1>(entity =>
        {
            entity.HasKey(e => e.Usuario);

            entity.ToTable("USUARIO", "SEGURIDAD");

            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .HasColumnName("USUARIO");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Cambiaclave).HasColumnName("CAMBIACLAVE");
            entity.Property(e => e.Clave).HasColumnName("CLAVE");
            entity.Property(e => e.Diascambioclave).HasColumnName("DIASCAMBIOCLAVE");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fechacambioclave)
                .HasColumnType("datetime")
                .HasColumnName("FECHACAMBIOCLAVE");
            entity.Property(e => e.Fechacreacion)
                .HasColumnType("datetime")
                .HasColumnName("FECHACREACION");
            entity.Property(e => e.Idagencia).HasColumnName("IDAGENCIA");
            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Puedeingresarsistema).HasColumnName("PUEDEINGRESARSISTEMA");
            entity.Property(e => e.Tienebloqueo).HasColumnName("TIENEBLOQUEO");
            entity.Property(e => e.Usadispositivomovil).HasColumnName("USADISPOSITIVOMOVIL");
        });

        modelBuilder.Entity<UsuarioRiesgos>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK_USUARIO_RLA_RLM");

            entity.ToTable("USUARIO_RIESGOSAPP", "SEGURIDAD");

            entity.Property(e => e.Usuario)
                .HasMaxLength(100)
                .HasColumnName("USUARIO");
            entity.Property(e => e.Clave).HasColumnName("CLAVE");

            entity.HasOne(d => d.UsuarioNavigation).WithOne(p => p.UsuarioRiesgosapp)
                .HasForeignKey<UsuarioRiesgos>(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_RLA_RLM");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CS_CAT.USUARIO_ROL");

            entity.ToTable("USUARIO_ROL", "CS_CAT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Codigorol)
                .HasMaxLength(50)
                .HasColumnName("CODIGOROL");
            entity.Property(e => e.Codigousuario)
                .HasMaxLength(50)
                .HasColumnName("CODIGOUSUARIO");

            entity.HasOne(d => d.CodigorolNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.Codigorol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CS_CAT.USUARIO_ROL_CS_CAT.ROL_CODIGOROL");

            entity.HasOne(d => d.CodigousuarioNavigation).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.Codigousuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CS_CAT.USUARIO_ROL_CS_CAT.USUARIO_CODIGOUSUARIO");
        });

        modelBuilder.Entity<UsuarioRol1>(entity =>
        {
            entity.ToTable("USUARIO_ROL", "SEGURIDAD");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Activo).HasColumnName("ACTIVO");
            entity.Property(e => e.Codigorol)
                .HasMaxLength(50)
                .HasColumnName("CODIGOROL");
            entity.Property(e => e.Usuarioregistro)
                .HasMaxLength(100)
                .HasColumnName("USUARIOREGISTRO");

            entity.HasOne(d => d.CodigorolNavigation).WithMany(p => p.UsuarioRol1s)
                .HasForeignKey(d => d.Codigorol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_ROL_ROL");

            entity.HasOne(d => d.UsuarioregistroNavigation).WithMany(p => p.UsuarioRol1s)
                .HasForeignKey(d => d.Usuarioregistro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_ROL_USUARIO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
