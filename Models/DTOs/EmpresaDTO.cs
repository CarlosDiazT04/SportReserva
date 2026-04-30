using System;

namespace SportReserva.Models.DTOs
{
    public class EmpresaDTO
    {
        public int EmpresaId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string RUC { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string? UrlMapa { get; set; }
        public string? UrlQR { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}