using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class CanchaMapper
    {
        public static CanchaDTO ToDTO(Cancha cancha)
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

        public static Cancha ToEntity(CanchaDTO canchaDTO)
        {
            return new Cancha
            {
                IdCancha = canchaDTO.IdCancha,
                Nombre = canchaDTO.Nombre,
                TipoDeporte = canchaDTO.TipoDeporte,
                PrecioHora = canchaDTO.PrecioHora,
                Estado = canchaDTO.Estado,
                Descripcion = canchaDTO.Descripcion
            };
        }
    }
}