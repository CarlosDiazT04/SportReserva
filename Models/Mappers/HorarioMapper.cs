using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class HorarioMapper
    {
        public static HorarioDTO ToDTO(Horario horario)
        {
            return new HorarioDTO
            {
                IdHorario = horario.IdHorario,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Estado = horario.Estado
            };
        }

        public static Horario ToEntity(HorarioDTO horarioDTO)
        {
            return new Horario
            {
                IdHorario = horarioDTO.IdHorario,
                HoraInicio = horarioDTO.HoraInicio,
                HoraFin = horarioDTO.HoraFin,
                Estado = horarioDTO.Estado
            };
        }
    }
}