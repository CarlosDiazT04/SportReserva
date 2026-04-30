using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Services
{
    public interface IHorarioService
    {
        IEnumerable<HorarioDTO> ObtenerTodos();
    }
}