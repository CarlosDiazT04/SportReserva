namespace SportReserva.Models.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string NombreUsuario { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;
    }
}