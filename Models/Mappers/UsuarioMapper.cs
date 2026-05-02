using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public static class UsuarioMapper
    {
        public static UsuarioDTO ToDTO(this Usuario entidad)
        {
            if (entidad == null) return null;
            
            return new UsuarioDTO { 
                IdUsuario = entidad.IdUsuario, 
                NombreUsuario = entidad.NombreUsuario, 
                Clave = entidad.Clave, 
                Rol = entidad.Rol 
            };
        }
    }
}