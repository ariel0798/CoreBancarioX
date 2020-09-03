using CB.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CB.Domain.Context
{
    public partial class CoreBancoDbContext : DbContext
    {
        public CoreBancoDbContext()
        {
        }

        public CoreBancoDbContext(DbContextOptions<CoreBancoDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Beneficiario> Beneficiario { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<CuentaAhorro> CuentaAhorro { get; set; }
        public virtual DbSet<HistorialTransaccion> HistorialTransaccion { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<TarjetaClave> TarjetaClave { get; set; }
        public virtual DbSet<TarjetaCredito> TarjetaCredito { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=PruebaLocalBanco");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficiario>(entity =>
            {
                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClienteBeneficiario)
                    .WithMany(p => p.BeneficiarioClienteBeneficiario)
                    .HasForeignKey(d => d.ClienteBeneficiarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__10216507");

                entity.HasOne(d => d.ClienteBeneficiarioProducto)
                    .WithMany(p => p.Beneficiario)
                    .HasForeignKey(d => d.ClienteBeneficiarioProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__11158940");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.BeneficiarioCliente)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__0F2D40CE");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.Cedula)
                    .HasName("UQ__Cliente__B4ADFE38E34D9072")
                    .IsUnique();

                entity.HasIndex(e => e.Usuario)
                    .HasName("UQ__Cliente__E3237CF7AE8737EE")
                    .IsUnique();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuentaAhorro>(entity =>
            {
                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TipoMoneda)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.CuentaAhorro)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CuentaAho__Produ__13F1F5EB");
            });

            modelBuilder.Entity<HistorialTransaccion>(entity =>
            {
                entity.Property(e => e.Descripcion)
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TipoTransaccion)
                    .HasMaxLength(7)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.HistorialTransaccion)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__Clien__22401542");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.HistorialTransaccion)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Historial__Produ__2334397B");

                entity.HasOne(d => d.Transaccion)
                    .WithMany(p => p.HistorialTransaccion)
                    .HasForeignKey(d => d.TransaccionId)
                    .HasConstraintName("FK__Historial__Trans__214BF109");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Titular)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.TitularId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Producto__Titula__0C50D423");
            });

            modelBuilder.Entity<TarjetaClave>(entity =>
            {
                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.TarjetaClave)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TarjetaCl__Clien__19AACF41");
            });

            modelBuilder.Entity<TarjetaCredito>(entity =>
            {
                entity.Property(e => e.Balance).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCorte).HasColumnType("date");

                entity.Property(e => e.LimiteCredito).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PagoCorte).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PagoMinimo).HasColumnType("decimal(19, 2)");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.TarjetaCredito)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__TarjetaCr__Produ__16CE6296");
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.Property(e => e.Comentario)
                    .IsUnicode(false);

                entity.Property(e => e.Descripcion)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Nota)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductoDestino)
                    .WithMany(p => p.TransaccionProductoDestino)
                    .HasForeignKey(d => d.ProductoDestinoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__Produ__1D7B6025");

                entity.HasOne(d => d.ProductoOrigen)
                    .WithMany(p => p.TransaccionProductoOrigen)
                    .HasForeignKey(d => d.ProductoOrigenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__Produ__1C873BEC");

                entity.HasOne(d => d.TarjetaClave)
                    .WithMany(p => p.Transaccion)
                    .HasForeignKey(d => d.TarjetaClaveId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__Tarje__1E6F845E");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
