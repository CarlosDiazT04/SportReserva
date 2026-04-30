using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportReserva.Models.Entities
{
    public class Cancha
    {
        public int IdCancha { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        public string TipoDeporte { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioHora { get; set; }

        public string Estado { get; set; } = "Disponible";

        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        [Required]
        public int EmpresaId { get; set; }

        [ForeignKey("EmpresaId")]
        public Empresa? Empresa { get; set; }
    }
}