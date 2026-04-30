// Models/Mappers/CanchaMapper.cs
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class CanchaMapper
    {
        public static CanchaDTO ToDto(Cancha cancha)
        {
            return new CanchaDTO
            {
                IdCancha = cancha.IdCancha,
                Nombre = cancha.Nombre,
                TipoDeporte = cancha.TipoDeporte,
                PrecioHora = cancha.PrecioHora,
                Estado = cancha.Estado,
                Descripcion = cancha.Descripcion
            };
        }

        public static Cancha ToEntity(CanchaDTO dto)
        {
            return new Cancha
            {
                IdCancha = dto.IdCancha,
                Nombre = dto.Nombre,
                TipoDeporte = dto.TipoDeporte,
                PrecioHora = dto.PrecioHora,
                Estado = dto.Estado,
                Descripcion = dto.Descripcion
            };
        }
    }
}