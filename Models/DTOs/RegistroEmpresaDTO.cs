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

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo no válido.")]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string? NumeroBilletera { get; set; }

        public string? UrlMapa { get; set; }
    }
}