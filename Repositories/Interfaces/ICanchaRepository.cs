using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface ICanchaRepository
    {
        IEnumerable<CanchaDTO> ObtenerTodas();
        CanchaDTO ObtenerPorId(int id);
        void Agregar(CanchaDTO cancha);
        void Actualizar(CanchaDTO cancha);
        void Desactivar(int id);
    }
}