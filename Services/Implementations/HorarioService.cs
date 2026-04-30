using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;
using System.Collections.Generic;

namespace SportReserva.Services.Implementations
{
    public class HorarioService : IHorarioService
    {
        private readonly IHorarioRepository _horarioRepository;

        public HorarioService(IHorarioRepository horarioRepository)
        {
            _horarioRepository = horarioRepository;
        }

        public IEnumerable<HorarioDTO> ObtenerTodos()
        {
            return _horarioRepository.ObtenerTodos();
        }
    }
}