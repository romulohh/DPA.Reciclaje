using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ReciclaDbContext _context;
        public UsuarioRepository(ReciclaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Usuario>> GetAllUsuarios()
        {
            return await _context.Usuario.ToListAsync();
        }
        public async Task<Usuario?> GetUsuarioById(int id)
        {
            return await _context.Usuario.Where(c => c.IdUsuario == id).FirstOrDefaultAsync();
        }
        public async Task<Usuario?> GetUsuarioByEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return null;
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }
        public async Task<int> AddUsuario(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();

            return usuario.IdUsuario;
        }
        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();

            var existingUsuario = await _context.Usuario.FindAsync(usuario.IdUsuario);
            if (existingUsuario == null)
            {
                return false;
            }
            existingUsuario.Nombres = usuario.Nombres;
            existingUsuario.Apellidos = usuario.Apellidos;
            existingUsuario.Direccion = usuario.Direccion;
            existingUsuario.IdDistrito = usuario.IdDistrito;
            existingUsuario.Rol = usuario.Rol;
            existingUsuario.Situacion = usuario.Situacion;
            existingUsuario.Estado = usuario.Estado;
            _context.Usuario.Update(existingUsuario);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
