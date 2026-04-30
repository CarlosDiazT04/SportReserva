// Models/DTOs/RegistroEmpresaDTO.cs
using System.ComponentModel.DataAnnotations;

namespace SportReserva.Models.DTOs
{
    public class RegistroEmpresaDTO
    {
        [Required]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        public string Clave { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; }
        
        [Required]
        public string RUC { get; set; }

        [Required]
        public string Direccion { get; set; }

        public string Telefono { get; set; }
    }
}