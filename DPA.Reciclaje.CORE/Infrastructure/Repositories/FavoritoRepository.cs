using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly ReciclaDbContext _context;
        public FavoritoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Favorito>> GetAllFavoritos()
        {
            return await _context.Favorito.ToListAsync();
        }

        public async Task<Favorito?> GetFavoritoById(int id)
        {
            return await _context.Favorito.Where(f => f.IdFavorito == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddFavorito(Favorito favorito)
        {
            await _context.Favorito.AddAsync(favorito);
            await _context.SaveChangesAsync();
            return favorito.IdFavorito;
        }

        public async Task<bool> DeleteFavoritosByProductoUsuario(int idProducto, int idUsuario)
        {
            var items = await _context.Favorito.Where(f => f.IdProducto == idProducto && f.IdUsuario == idUsuario).ToListAsync();
            if (items == null || items.Count == 0) return false;

            _context.Favorito.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
