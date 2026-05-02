using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SportReserva.Data;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly Conexion _context;

        public ReporteRepository(Conexion context)
        {
            _context = context;
        }

        public IEnumerable<ReporteReservaDTO> GenerarReporteGeneral()
        {
            return _context.Reservas
                .Include(r => r.Cliente)
                .Include(r => r.Cancha)
                .Include(r => r.Horario)
                .Include(r => r.Pago)
                .OrderByDescending(r => r.FechaRegistro)
                .Select(r => new ReporteReservaDTO
                {
                    IdReserva = r.IdReserva,
                    Cliente = r.Cliente != null ? $"{r.Cliente.Nombres} {r.Cliente.Apellidos}" : "Sin cliente",
                    Cancha = r.Cancha != null ? r.Cancha.Nombre : "Sin cancha",
                    TipoDeporte = r.Cancha != null ? r.Cancha.TipoDeporte : "N/A",
                    FechaReserva = r.FechaReserva,
                    HoraInicio = r.Horario != null ? r.Horario.HoraInicio : default,
                    HoraFin = r.Horario != null ? r.Horario.HoraFin : default,
                    PrecioTotal = r.PrecioTotal,
                    EstadoReserva = r.EstadoReserva,
                    EstadoPago = r.Pago != null ? r.Pago.EstadoPago : "Pendiente"
                })
                .ToList();
        }
    }
}
