using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Mappers
{
    public static class ReservaMapper
    {
        public static ReservaDTO ToDTO(this Reserva entidad)
        {
            if (entidad == null) return null;

            return new ReservaDTO
            {
                IdReserva = entidad.IdReserva,
                IdCliente = entidad.IdCliente,
                IdCancha = entidad.IdCancha,
                IdHorario = entidad.IdHorario,
                FechaReserva = entidad.FechaReserva,
                FechaRegistro = entidad.FechaRegistro,
                PrecioTotal = entidad.PrecioTotal,
                EstadoReserva = entidad.EstadoReserva
            };
        }
    }
}