using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IHorarioRepository
    {
        IEnumerable<HorarioDTO> ObtenerTodos();
        HorarioDTO ObtenerPorId(int id);
        void Agregar(HorarioDTO horario);
        void Actualizar(HorarioDTO horario);
        void Eliminar(int id);
    }
}