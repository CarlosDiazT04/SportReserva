namespace SportReserva.Models.DTOs
{
    public class RegistroDTO
    {
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string DNI { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }
}