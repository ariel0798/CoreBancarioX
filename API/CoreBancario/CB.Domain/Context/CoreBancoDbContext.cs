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
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<TarjetaCredito> TarjetaCredito { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=CoreBancarioData");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beneficiario>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Alias)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClienteBeneficiario)
                    .WithMany()
                    .HasForeignKey(d => d.ClienteBeneficiarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__5E8A0973");

                entity.HasOne(d => d.ClienteBeneficiarioProducto)
                    .WithMany()
                    .HasForeignKey(d => d.ClienteBeneficiarioProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__5F7E2DAC");

                entity.HasOne(d => d.Cliente)
                    .WithMany()
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Beneficia__Clien__5D95E53A");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasIndex(e => e.Cedula)
                    .HasName("UQ__Cliente__B4ADFE38F1F5469D")
                    .IsUnique();

                entity.Property(e => e.Apellidos)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CuentaAhorro>(entity =>
            {
                entity.HasKey(e => e.ProductoId)
                    .HasName("PK__CuentaAh__A430AEA39C43F4F5");

                entity.Property(e => e.ProductoId).ValueGeneratedNever();

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TipoMoneda)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);
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
                    .HasConstraintName("FK__Producto__Titula__5BAD9CC8");
            });

            modelBuilder.Entity<TarjetaCredito>(entity =>
            {
                entity.HasKey(e => e.ProductoId)
                    .HasName("PK__TarjetaC__A430AEA3080A4C1B");

                entity.Property(e => e.ProductoId).ValueGeneratedNever();

                entity.Property(e => e.Balance).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaCorte).HasColumnType("date");

                entity.Property(e => e.LimiteCredito).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PagoCorte).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.PagoMinimo).HasColumnType("decimal(19, 2)");
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.Property(e => e.Descripccion)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FechaTransaccion).HasColumnType("datetime");

                entity.Property(e => e.Monto).HasColumnType("decimal(19, 2)");

                entity.Property(e => e.TipoTransaccion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductoDestino)
                    .WithMany(p => p.TransaccionProductoDestino)
                    .HasForeignKey(d => d.ProductoDestinoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__Produ__671F4F74");

                entity.HasOne(d => d.ProductoOrigen)
                    .WithMany(p => p.TransaccionProductoOrigen)
                    .HasForeignKey(d => d.ProductoOrigenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacci__Produ__662B2B3B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
