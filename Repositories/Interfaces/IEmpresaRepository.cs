using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Repositories.Interfaces
{
    public interface IEmpresaRepository
    {
        Task AgregarAsync(EmpresaDTO empresa);
        Task AgregarAsync(Empresa empresa);
        Task<EmpresaDTO?> ObtenerPorIdAsync(int id);
        Task<IEnumerable<EmpresaDTO>> ObtenerTodasAsync();
    }
}