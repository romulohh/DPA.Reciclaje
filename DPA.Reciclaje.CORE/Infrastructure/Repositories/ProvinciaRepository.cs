using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class ProvinciaRepository : IProvinciaRepository
    {
        private readonly ReciclaDbContext _context;
        public ProvinciaRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Provincia>> GetAllProvincias()
        {
            return await _context.Provincia.ToListAsync();
        }

        public async Task<Provincia?> GetProvinciaById(int id)
        {
            return await _context.Provincia.Where(p => p.IdProvincia == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddProvincia(Provincia provincia)
        {
            await _context.Provincia.AddAsync(provincia);
            await _context.SaveChangesAsync();
            return provincia.IdProvincia;
        }

        public async Task<IEnumerable<Provincia>> GetProvinciasByDepartamento(int departamentoId)
        {
            return await _context.Provincia.Where(p => p.IdDepartamento == departamentoId).ToListAsync();
        }
    }
}
