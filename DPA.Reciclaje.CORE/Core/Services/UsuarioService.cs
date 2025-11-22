using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DPA.Reciclaje.CORE.Infrastructure.Shared;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJWTService _jwtService;
        public UsuarioService(IUsuarioRepository usuarioRepository, IJWTService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _jwtService = jwtService;
        }
        public async Task<UsuarioResponseDTO?> SignInAsync(SignInDTO dto)
        {
            var usuario = await _usuarioRepository.GetUsuarioByEmail(dto.Email);
            if (usuario == null) return null;

            var hashed = HashPassword(dto.Clave);
            if (usuario.Clave != hashed) return null;

            var token = _jwtService.GenerateJWToken(usuario);

            return new UsuarioResponseDTO
            {
                IdUsuario = usuario.IdUsuario,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Email = usuario.Email,
                Estado = usuario.Estado,
                Situacion = usuario.Situacion,
                Rol = usuario.Rol,
                Token = token
            };
        }
        public async Task<int> SignUpAsync(SignUpDTO dto)
        {
            var existing = await _usuarioRepository.GetUsuarioByEmail(dto.Email);
            if (existing != null)
            {
                return 0;
            }

            var usuario = new Usuario
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Email = dto.Email,
                Direccion = dto.Direccion,
                IdDistrito = dto.IdDistrito,
                Rol = dto.Rol,
                Clave = HashPassword(dto.Clave),
                Puntuacion = "X",
                Situacion = "C",
                Estado = "A",
                UltAccionVenta = "X",
                UltAccionCompra = "X"
            };

            var id = await _usuarioRepository.AddUsuario(usuario);
            return id;
        }
        private static string HashPassword(string password)
        {
            if (password == null) return string.Empty;
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (var b in hash) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
