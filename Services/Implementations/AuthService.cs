using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportReserva.Models.DTOs;
using SportReserva.Repositories.Interfaces;

namespace SportReserva.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IClienteRepository _clienteRepo;
        private readonly IEmpresaRepository _empresaRepo;

        public AuthService(IUsuarioRepository usuarioRepo, IClienteRepository clienteRepo, IEmpresaRepository empresaRepo)
        {
            _usuarioRepo = usuarioRepo;
            _clienteRepo = clienteRepo;
            _empresaRepo = empresaRepo;
        }

        public async Task<ClaimsPrincipal> Authenticate(LoginDTO loginDTO)
        {
            var usuario = _usuarioRepo.ObtenerPorNombreUsuario(loginDTO.NombreUsuario);
            
            if (usuario == null) return null;

            var hasher = new PasswordHasher<string>();
            var validacion = hasher.VerifyHashedPassword(null, usuario.Clave, loginDTO.Clave);
            if (validacion == PasswordVerificationResult.Failed) return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString())
            };

            if (usuario.Rol == "Cliente")
            {
                var clientes = await _clienteRepo.ObtenerTodosAsync();
                var cliente = clientes.FirstOrDefault(c => c.IdUsuario == usuario.IdUsuario);
                if (cliente != null) claims.Add(new Claim("IdCliente", cliente.IdCliente.ToString()));
            }
            else if (usuario.Rol == "Empresa")
            {
                var empresas = await _empresaRepo.ObtenerTodasAsync();
                var empresa = empresas.FirstOrDefault(e => e.IdUsuario == usuario.IdUsuario);
                if (empresa != null) claims.Add(new Claim("IdEmpresa", empresa.EmpresaId.ToString()));
            }

            var identity = new ClaimsIdentity(claims, "CookieAuth");
            return new ClaimsPrincipal(identity);
        }

        public async Task<ResultadoRegistroDTO> RegistrarClienteAsync(RegistroDTO dto)
        {
            var usuarioExistente = _usuarioRepo.ObtenerPorNombreUsuario(dto.NombreUsuario);
            if (usuarioExistente != null)
            {
                return new ResultadoRegistroDTO { Exito = false, Mensaje = "El nombre de usuario ya está en uso." };
            }

            var hasher = new PasswordHasher<string>();
            string claveHasheada = hasher.HashPassword(null, dto.Clave);

            var nuevoUsuario = new UsuarioDTO { NombreUsuario = dto.NombreUsuario, Clave = claveHasheada, Rol = "Cliente" };
            _usuarioRepo.Agregar(nuevoUsuario);

            var nuevoCliente = new ClienteDTO
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                DNI = dto.DNI,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                IdUsuario = nuevoUsuario.IdUsuario
            };
            
            _clienteRepo.Agregar(nuevoCliente);

            await Task.CompletedTask;
            return new ResultadoRegistroDTO { Exito = true };
        }

        public async Task<ResultadoRegistroDTO> RegistrarEmpresaAsync(RegistroEmpresaDTO dto)
        {
            var usuarioExistente = _usuarioRepo.ObtenerPorNombreUsuario(dto.NombreUsuario);
            if (usuarioExistente != null)
            {
                return new ResultadoRegistroDTO { Exito = false, Mensaje = "El nombre de usuario de la empresa ya está en uso." };
            }

            var hasher = new PasswordHasher<string>();
            string claveHasheada = hasher.HashPassword(null, dto.Clave);

            var nuevoUsuario = new UsuarioDTO { NombreUsuario = dto.NombreUsuario, Clave = claveHasheada, Rol = "Empresa" };
            _usuarioRepo.Agregar(nuevoUsuario);

            var nuevaEmpresa = new EmpresaDTO
            {
                Nombre = dto.Nombre,
                RUC = dto.RUC,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                UrlMapa = dto.UrlMapa,
                NumeroBilletera = dto.NumeroBilletera,
                IdUsuario = nuevoUsuario.IdUsuario,
                FechaRegistro = DateTime.Now
            };

            try
            {
                await _empresaRepo.AgregarAsync(nuevaEmpresa);
                return new ResultadoRegistroDTO { Exito = true };
            }
            catch (Exception ex)
            {
                // Si el guardado falla, avisamos al controlador.
                // En una app robusta, aquí es ideal aplicar un Rollback o borrar el usuario recién creado.
                return new ResultadoRegistroDTO { Exito = false, Mensaje = "Ocurrió un error al registrar los datos de la empresa. Revisa la información." };
            }
        }
    }
}