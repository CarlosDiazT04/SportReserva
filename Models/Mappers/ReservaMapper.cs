using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class ReservaMapper
    {
        public static ReservaDTO ToDTO(Reserva reserva)
        {
            return new ReservaDTO
            {
                IdReserva = reserva.IdReserva,
                IdCliente = reserva.IdCliente,
                IdCancha = reserva.IdCancha,
                IdHorario = reserva.IdHorario,
                FechaReserva = reserva.FechaReserva,
                FechaRegistro = reserva.FechaRegistro,
                PrecioTotal = reserva.PrecioTotal,
                EstadoReserva = reserva.EstadoReserva
            };
        }

        public static Reserva ToEntity(ReservaDTO reservaDTO)
        {
            return new Reserva
            {
                IdReserva = reservaDTO.IdReserva,
                IdCliente = reservaDTO.IdCliente,
                IdCancha = reservaDTO.IdCancha,
                IdHorario = reservaDTO.IdHorario,
                FechaReserva = reservaDTO.FechaReserva,
                FechaRegistro = reservaDTO.FechaRegistro,
                PrecioTotal = reservaDTO.PrecioTotal,
                EstadoReserva = reservaDTO.EstadoReserva
            };
        }

        public static Reserva ToEntityFromRegistrar(RegistrarReservaDTO registrarReservaDTO)
        {
            return new Reserva
            {
                IdCliente = registrarReservaDTO.IdCliente,
                IdCancha = registrarReservaDTO.IdCancha,
                IdHorario = registrarReservaDTO.IdHorario,
                FechaReserva = registrarReservaDTO.FechaReserva,
                FechaRegistro = DateTime.Now,
                EstadoReserva = "Pendiente"
            };
        }
    }
}