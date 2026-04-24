namespace SportReserva.Models.DTOs
{
    public class PagoDTO
    {
        public int IdPago { get; set; }

        public int IdReserva { get; set; }

        public decimal Monto { get; set; }

        public string MetodoPago { get; set; } = string.Empty;

        public string EstadoPago { get; set; } = string.Empty;

        public DateTime? FechaPago { get; set; }
    }
}