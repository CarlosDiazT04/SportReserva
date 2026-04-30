﻿namespace SportReserva.Models.DTOs
{
    public class CanchaDTO
    {
        public int IdCancha { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string TipoDeporte { get; set; } = string.Empty;

        public decimal PrecioHora { get; set; }

        public string Estado { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int EmpresaId { get; set; }
    }
}