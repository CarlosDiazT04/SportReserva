using System.ComponentModel.DataAnnotations;

namespace SportReserva.Models.Entities
{
    public class Usuario
    {
        [Key]
        public int IdUsuario { get; set; }
        
        public string NombreUsuario { get; set; } = string.Empty;
        
        public string Clave { get; set; } = string.Empty;
        
        public string Rol { get; set; } = string.Empty;
        
        public string Estado { get; set; } = "Activo";

        public Cliente Cliente { get; set; }
    }
}
