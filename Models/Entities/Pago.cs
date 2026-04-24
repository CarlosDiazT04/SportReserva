using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportReserva.Models.Entities
{
    public class Pago
    {
        [Key]
        public int IdPago { get; set; }

        public int IdReserva { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Monto { get; set; }

        [Required]
        [StringLength(30)]
        public string MetodoPago { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string EstadoPago { get; set; } = string.Empty;

        public DateTime? FechaPago { get; set; }
    }
}