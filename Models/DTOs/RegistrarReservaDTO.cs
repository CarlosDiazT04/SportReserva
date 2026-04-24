namespace SportReserva.Models.DTOs
{
    public class RegistrarReservaDTO
    {
        public int IdCliente { get; set; }

        public int IdCancha { get; set; }

        public int IdHorario { get; set; }

        public DateTime FechaReserva { get; set; }
    }
}