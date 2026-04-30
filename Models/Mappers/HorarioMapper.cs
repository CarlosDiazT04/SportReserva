using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Mappers
{
    public static class HorarioMapper
    {
        public static HorarioDTO ToDTO(this Horario entidad)
        {
            if (entidad == null) return null;
            
            return new HorarioDTO { 
                IdHorario = entidad.IdHorario, 
                HoraInicio = entidad.HoraInicio, 
                HoraFin = entidad.HoraFin, 
                Estado = entidad.Estado 
            };
        }
    }
}