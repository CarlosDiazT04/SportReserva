using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportReserva.Models.Entities
{
    public class Cancha
    {
        [Key]
        public int IdCancha { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string TipoDeporte { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal PrecioHora { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;

        [StringLength(250)]
        public string Descripcion { get; set; } = string.Empty;
    }
}