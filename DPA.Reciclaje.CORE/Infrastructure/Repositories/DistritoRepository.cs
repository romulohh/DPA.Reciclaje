using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class DistritoRepository : IDistritoRepository
    {
        private readonly ReciclaDbContext _context;
        public DistritoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Distrito>> GetAllDistritos()
        {
            return await _context.Distrito.ToListAsync();
        }

        public async Task<Distrito?> GetDistritoById(int id)
        {
            return await _context.Distrito.Where(d => d.IdDistrito == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddDistrito(Distrito distrito)
        {
            await _context.Distrito.AddAsync(distrito);
            await _context.SaveChangesAsync();
            return distrito.IdDistrito;
        }

        public async Task<IEnumerable<Distrito>> GetDistritosByProvincia(int provinciaId)
        {
            return await _context.Distrito.Where(d => d.IdProvincia == provinciaId).ToListAsync();
        }

        public async Task<IEnumerable<Distrito>> GetDistritosByDepartamento(int departamentoId)
        {
            return await _context.Distrito.Where(d => d.IdDepartamento == departamentoId).ToListAsync();
        }
    }
}
