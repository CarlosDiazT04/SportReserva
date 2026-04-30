// Services/Implementations/EmpresaService.cs
using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;
using SportReserva.Models.Mappers;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICanchaRepository _canchaRepository;

        public EmpresaService(IEmpresaRepository empresaRepository, ICanchaRepository canchaRepository)
        {
            _empresaRepository = empresaRepository;
            _canchaRepository = canchaRepository;
        }

        public IEnumerable<CanchaDTO> ObtenerCanchasDeEmpresa(int idEmpresa)
        {
            var canchas = _canchaRepository.ObtenerPorEmpresa(idEmpresa);
            return canchas; // Ya devuelve una lista de CanchaDTO desde el Repositorio
        }

        public void AgregarCancha(CanchaDTO canchaDTO)
        {
            _canchaRepository.Agregar(canchaDTO);
        }

        public async Task RegistrarEmpresa(RegistroEmpresaDTO dto)
        {
            var empresa = new Empresa
            {
                Nombre = dto.Nombre,
                RUC = dto.RUC,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono
            };

            await _empresaRepository.AgregarAsync(empresa);
        }
    }
}