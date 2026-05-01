using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SportReserva.Models.Entities
{
    public class Empresa
    {
        [Key]
        public int EmpresaId { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string RUC { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(250)]
        public string Direccion { get; set; }

        public DateTime FechaRegistro { get; set; }

        [StringLength(50)]
        public string Telefono { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public ICollection<Cancha> Canchas { get; set; }

        public string? UrlMapa { get; set; }
        public string? NumeroBilletera { get; set; }

        public TimeSpan? HoraApertura { get; set; }
        public TimeSpan? HoraCierre { get; set; }
    }
}