using SportReserva.Models.Entities;
using SportReserva.Models.DTOs;

namespace SportReserva.Services
{
    public interface IEmpresaService
    {
        IEnumerable<CanchaDTO> ObtenerCanchasDeEmpresa(int idEmpresa);
        void AgregarCancha(CanchaDTO canchaDTO);
        Task RegistrarEmpresa(RegistroEmpresaDTO dto);
        Task<IEnumerable<Empresa>> ObtenerTodas();
    }
}