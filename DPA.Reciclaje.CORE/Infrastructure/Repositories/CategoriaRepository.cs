using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ReciclaDbContext _context;
        public CategoriaRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categoría>> GetAllCategorias()
        {
            return await _context.Categoría.ToListAsync();
        }

        public async Task<Categoría?> GetCategoriaById(int id)
        {
            return await _context.Categoría.Where(c => c.IdCategoria == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddCategoria(Categoría categoria)
        {
            await _context.Categoría.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria.IdCategoria;
        }
    }
}
