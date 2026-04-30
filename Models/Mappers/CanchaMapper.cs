using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Mappers
{
    public static class CanchaMapper
    {
        public static CanchaDTO ToDTO(this Cancha entidad)
        {
            if (entidad == null) return null;
            
            return new CanchaDTO
            {
                IdCancha = entidad.IdCancha,
                Nombre = entidad.Nombre,
                TipoDeporte = entidad.TipoDeporte,
                PrecioHora = entidad.PrecioHora,
                Estado = entidad.Estado,
                Descripcion = entidad.Descripcion,
                EmpresaId = entidad.EmpresaId
            };
        }
    }
}