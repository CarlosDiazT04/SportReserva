namespace SportReserva.Models.DTOs
{
    public class HorarioDTO
    {
        public int IdHorario { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public string Estado { get; set; } = string.Empty;
    }
}