namespace SportReserva.Models.DTOs
{
    public class ResultadoRegistroDTO
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public object? Datos { get; set; }
    }
}