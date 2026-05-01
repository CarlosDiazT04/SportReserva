using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Services
{
    public interface ICanchaService
    {
        IEnumerable<CanchaDTO> ObtenerTodas();
        IEnumerable<CanchaDTO> ObtenerPorEmpresa(int empresaId);
    }
}