using SportReserva.Models.DTOs;
using System.Collections.Generic;

namespace SportReserva.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioDTO> ObtenerTodos();
        UsuarioDTO ObtenerPorId(int id);
        UsuarioDTO? ObtenerPorNombreUsuario(string username);
        void Agregar(UsuarioDTO usuario);
        void Actualizar(UsuarioDTO usuario);
        void Desactivar(int id);
    }
}