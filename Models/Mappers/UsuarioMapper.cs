using SportReserva.Models.DTOs;
using SportReserva.Models.Entities;

namespace SportReserva.Models.Mappers
{
    public class UsuarioMapper
    {
        public static UsuarioDTO ToDTO(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Rol = usuario.Rol,
                Estado = usuario.Estado
            };
        }

        public static Usuario ToEntity(UsuarioDTO usuarioDTO)
        {
            return new Usuario
            {
                IdUsuario = usuarioDTO.IdUsuario,
                NombreUsuario = usuarioDTO.NombreUsuario,
                Rol = usuarioDTO.Rol,
                Estado = usuarioDTO.Estado
            };
        }
    }
}