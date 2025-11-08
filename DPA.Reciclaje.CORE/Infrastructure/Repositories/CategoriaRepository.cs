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

        public async Task<IEnumerable<Categoria>> GetAllCategorias()
        {
            return await _context.Categoria.ToListAsync();
        }

        public async Task<Categoria?> GetCategoriaById(int id)
        {
            return await _context.Categoria.Where(c => c.IdCategoria == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddCategoria(Categoria categoria)
        {
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();
            return categoria.IdCategoria;
        }
    }
}
