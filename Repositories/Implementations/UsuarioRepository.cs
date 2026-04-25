using System.Linq;
using SportReserva.Data;
using SportReserva.Models.Entities;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Conexion _context;

        public UsuarioRepository(Conexion context)
        {
            _context = context;
        }

        public UsuarioDTO? ValidarLogin(string username, string clave)
        {
            var usuarioDb = _context.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == username && u.Clave == clave);
            
            if (usuarioDb == null) return null;

            // 3. Si existe, lo empaquetamos en el DTO para el Controlador
            return new UsuarioDTO
            {
                IdUsuario = usuarioDb.IdUsuario,
                NombreUsuario = usuarioDb.NombreUsuario,
                Clave = usuarioDb.Clave,
                Rol = usuarioDb.Rol
            };
        }
        public void Actualizar(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public void Agregar(UsuarioDTO usuario)
        {
            throw new NotImplementedException();
        }

        public void Desactivar(int id)
        {
            throw new NotImplementedException();
        }

        public UsuarioDTO ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UsuarioDTO> ObtenerTodos()
        {
            throw new NotImplementedException();
        }

    }
}