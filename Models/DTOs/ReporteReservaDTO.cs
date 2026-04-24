namespace SportReserva.Models.DTOs
{
    public class ReporteReservaDTO
    {
        public int IdReserva { get; set; }

        public string Cliente { get; set; } = string.Empty;

        public string Cancha { get; set; } = string.Empty;

        public string TipoDeporte { get; set; } = string.Empty;

        public DateTime FechaReserva { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraFin { get; set; }

        public decimal PrecioTotal { get; set; }

        public string EstadoReserva { get; set; } = string.Empty;

        public string EstadoPago { get; set; } = string.Empty;
    }
}