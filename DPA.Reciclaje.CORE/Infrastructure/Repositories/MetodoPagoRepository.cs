using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class MetodoPagoRepository : IMetodoPagoRepository
    {
        private readonly ReciclaDbContext _context;
        public MetodoPagoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetodoPago>> GetAllMetodoPagos()
        {
            return await _context.MetodoPago.ToListAsync();
        }

        public async Task<MetodoPago?> GetMetodoPagoById(int id)
        {
            return await _context.MetodoPago.Where(m => m.IdMetodoPago == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddMetodoPago(MetodoPago metodoPago)
        {
            await _context.MetodoPago.AddAsync(metodoPago);
            await _context.SaveChangesAsync();
            return metodoPago.IdMetodoPago;
        }
    }
}