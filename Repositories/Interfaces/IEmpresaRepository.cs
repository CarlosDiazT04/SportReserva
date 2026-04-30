using SportReserva.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        Task AgregarAsync(Empresa empresa);
        Task<Empresa?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<Empresa>> ObtenerTodasAsync();
    }
}