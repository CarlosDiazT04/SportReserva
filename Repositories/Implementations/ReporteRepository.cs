using System;
using System.Collections.Generic;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class ReporteRepository : IReporteRepository
    {
        public IEnumerable<ReporteReservaDTO> GenerarReporteGeneral()
        {
            throw new NotImplementedException();
        }
    }
}