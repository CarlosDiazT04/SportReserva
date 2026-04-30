using Microsoft.EntityFrameworkCore;
using SportReserva.Models.Entities;

namespace SportReserva.Data
{
    public class Conexion : DbContext
    {
        public Conexion(DbContextOptions<Conexion> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cancha> Canchas { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>().ToTable("Empresa");
            modelBuilder.Entity<Empresa>().HasKey(e => e.EmpresaId);

            modelBuilder.Entity<Empresa>()
            .HasOne(e => e.Usuario)
            .WithOne(u => u.Empresa)
            .HasForeignKey<Empresa>(e => e.IdUsuario)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Empresa>()
                .HasIndex(e => e.RUC)
                .IsUnique();

            modelBuilder.Entity<Cancha>()
                .HasOne(c => c.Empresa)
                .WithMany(e => e.Canchas)
                .HasForeignKey(c => c.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cancha>().ToTable("Cancha");
            modelBuilder.Entity<Horario>().ToTable("Horario");
            modelBuilder.Entity<Reserva>().ToTable("Reserva");
            modelBuilder.Entity<Pago>().ToTable("Pago");

            modelBuilder.Entity<Usuario>().HasKey(u => u.IdUsuario);
            modelBuilder.Entity<Cliente>().HasKey(c => c.IdCliente);
            modelBuilder.Entity<Cancha>().HasKey(c => c.IdCancha);
            modelBuilder.Entity<Horario>().HasKey(h => h.IdHorario);
            modelBuilder.Entity<Reserva>().HasKey(r => r.IdReserva);
            modelBuilder.Entity<Pago>().HasKey(p => p.IdPago);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NombreUsuario)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.DNI)
                .IsUnique();

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)
                .WithOne(u => u.Cliente)
                .HasForeignKey<Cliente>(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cliente)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.IdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Cancha)
                .WithMany(c => c.Reservas)
                .HasForeignKey(r => r.IdCancha)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasOne(r => r.Horario)
                .WithMany(h => h.Reservas)
                .HasForeignKey(r => r.IdHorario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Pago>()
                .HasOne(p => p.Reserva)
                .WithOne(r => r.Pago)
                .HasForeignKey<Pago>(p => p.IdReserva)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reserva>()
                .HasIndex(r => new { r.IdCancha, r.FechaReserva, r.IdHorario })
                .IsUnique()
                .HasFilter("[EstadoReserva] <> 'Cancelada'");
        }
    }
}