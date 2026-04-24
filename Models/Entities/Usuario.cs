using System.ComponentModel.DataAnnotations;

namespace SportReserva.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Clave { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Rol { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;

        public Cliente? Cliente { get; set; }
    }
}