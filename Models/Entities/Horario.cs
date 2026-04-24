using System.ComponentModel.DataAnnotations;

namespace SportReserva.Models.Entities
{
    public class Horario
    {
        [Key]
        public int IdHorario { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        [Required]
        [StringLength(20)]
        public string Estado { get; set; } = string.Empty;
    }
}