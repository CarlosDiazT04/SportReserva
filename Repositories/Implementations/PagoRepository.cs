using System.Collections.Generic;
using System.Linq;
using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class PagoRepository : IPagoRepository
    {
        private readonly Conexion _context;

        public PagoRepository(Conexion context)
        {
            _context = context;
        }

        public IEnumerable<PagoDTO> ObtenerTodos()
        {
            return _context.Pagos.Select(p => new PagoDTO
            {
                IdPago = p.IdPago,
                IdReserva = p.IdReserva,
                Monto = p.Monto,
                MetodoPago = p.MetodoPago,
                EstadoPago = p.EstadoPago,
                FechaPago = p.FechaPago
            }).ToList();
        }

        public PagoDTO ObtenerPorId(int id)
        {
            var pago = _context.Pagos.Find(id);
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

        public void Agregar(PagoDTO pago)
        {
            var entidad = new Pago
            {
                IdReserva = pago.IdReserva,
                Monto = pago.Monto,
                MetodoPago = pago.MetodoPago,
                EstadoPago = pago.EstadoPago,
                FechaPago = pago.FechaPago
            };

            _context.Pagos.Add(entidad);
            _context.SaveChanges();
        }

        public void Actualizar(PagoDTO pago)
        {
            var entidad = _context.Pagos.Find(pago.IdPago);
            if (entidad == null) return;

            entidad.IdReserva = pago.IdReserva;
            entidad.Monto = pago.Monto;
            entidad.MetodoPago = pago.MetodoPago;
            entidad.EstadoPago = pago.EstadoPago;
            entidad.FechaPago = pago.FechaPago;

            _context.SaveChanges();
        }
    }
}
