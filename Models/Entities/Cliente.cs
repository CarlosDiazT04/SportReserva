using System.ComponentModel.DataAnnotations;

namespace SportReserva.Models.Entities
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [StringLength(80)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(8)]
        public string DNI { get; set; } = string.Empty;

        [StringLength(15)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(100)]
        public string Correo { get; set; } = string.Empty;

        public int IdUsuario { get; set; }

        public Usuario? Usuario { get; set; }

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}