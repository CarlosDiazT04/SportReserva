using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class PagoMapper
    {
        public static PagoDTO ToDTO(Pago pago)
        {
            if (pago == null) return null;

            return new PagoDTO
            {
                IdPago = pago.IdPago,
                IdReserva = pago.IdReserva,
                Monto = pago.Monto,
                MetodoPago = pago.MetodoPago,
                EstadoPago = pago.EstadoPago,
                FechaPago = pago.FechaPago
            };
        }

        public static Pago ToEntity(PagoDTO pagoDTO)
        {
            if (pagoDTO == null) return null;

            return new Pago
            {
                IdPago = pagoDTO.IdPago,
                IdReserva = pagoDTO.IdReserva,
                Monto = pagoDTO.Monto,
                MetodoPago = pagoDTO.MetodoPago,
                EstadoPago = pagoDTO.EstadoPago,
                FechaPago = pagoDTO.FechaPago
            };
        }
    }
}