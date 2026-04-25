using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IReporteRepository
    {
        IEnumerable<ReporteReservaDTO> GenerarReporteGeneral();
    }
}