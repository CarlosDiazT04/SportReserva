using SportReserva.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SportReserva.Services
{
    public interface IEmpresaService
    {
        Task<IEnumerable<EmpresaDTO>> ObtenerTodasAsync();
        Task<EmpresaDTO?> ObtenerPorIdAsync(int id);
        Task<EmpresaDTO?> ObtenerPorUsuarioIdAsync(int idUsuario);
        IEnumerable<CanchaDTO> ObtenerCanchasDeEmpresa(int idEmpresa);
        void AgregarCancha(CanchaDTO canchaDTO);
    }
}