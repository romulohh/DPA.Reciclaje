using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ReciclaDbContext _context;
        public ComentarioRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddComentario(Comentario comentario)
        {
            comentario.Fecha = DateTime.Now;
            await _context.Comentario.AddAsync(comentario);
            await _context.SaveChangesAsync();
            return comentario.IdComentario;
        }

        public async Task<int> AddComentarioImagen(ComentarioImg img)
        {
            await _context.ComentarioImg.AddAsync(img);
            await _context.SaveChangesAsync();
            return img.IdComentarioImg;
        }

        public async Task<IEnumerable<Comentario>> GetAllComentarios()
        {
            return await _context.Comentario
                .Include(c => c.ComentarioImg)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetComentariosByProductoAndComprador(int idProducto, int idUsuarioComprador)
        {
            return await _context.Comentario
                .Include(c => c.ComentarioImg)
                .Where(c => c.IdProducto == idProducto && c.IdUsuarioComprador == idUsuarioComprador)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Comentario>> GetComentariosByProductoAndVendedor(int idProducto, int idUsuarioVendedor)
        {
            return await _context.Comentario
                .Include(c => c.ComentarioImg)
                .Where(c => c.IdProducto == idProducto && c.IdUsuarioVendedor == idUsuarioVendedor)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Comentario?> GetComentarioById(int id)
        {
            return await _context.Comentario
                .Include(c => c.ComentarioImg)
                .Where(c => c.IdComentario == id)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateComentario(Comentario comentario)
        {
            var existing = await _context.Comentario.FindAsync(comentario.IdComentario);
            if (existing == null) return false;

            existing.Texto = comentario.Texto;
            existing.Calificacion = comentario.Calificacion;
            existing.Estado = comentario.Estado;
            _context.Comentario.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
