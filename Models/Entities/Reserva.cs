using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportReserva.Models.Entities
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        public int IdCliente { get; set; }

        public int IdCancha { get; set; }

        public int IdHorario { get; set; }

        public DateTime FechaReserva { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioTotal { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoReserva { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }

        public Cancha? Cancha { get; set; }

        public Horario? Horario { get; set; }

        public Pago? Pago { get; set; }
    }
}