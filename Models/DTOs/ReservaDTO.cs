namespace SportReserva.Models.DTOs
{
    public class ReservaDTO
    {
        public int IdReserva { get; set; }
        public int IdCliente { get; set; }
        public int IdCancha { get; set; }
        public int IdHorario { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime FechaRegistro { get; set; }
        public decimal PrecioTotal { get; set; }
        public string EstadoReserva { get; set; } = string.Empty;

        public string CanchaNombre { get; set; } = string.Empty;
        public string HorarioTexto { get; set; } = string.Empty;
    }
}
