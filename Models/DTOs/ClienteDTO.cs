namespace SportReserva.Models.DTOs
{
    public class ClienteDTO
    {
        public int IdCliente { get; set; }

        public string Nombres { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string DNI { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public int IdUsuario { get; set; }
    }
}