using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly ReciclaDbContext _context;
        public PagoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pago>> GetAllPagos()
        {
            return await _context.Pago
                .Include(p => p.IdCarritoNavigation)
                    .ThenInclude(c => c.CarritoProducto)
                        .ThenInclude(ci => ci.IdProductoNavigation)
                .Include(p => p.IdMetodoPagoNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Pago?> GetPagoById(int id)
        {
            return await _context.Pago
                .Include(p => p.IdCarritoNavigation)
                    .ThenInclude(c => c.CarritoProducto)
                        .ThenInclude(ci => ci.IdProductoNavigation)
                .Include(p => p.IdMetodoPagoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.IdPago == id);
        }

        public async Task<int> AddPago(Pago pago)
        {
            await _context.Pago.AddAsync(pago);
            await _context.SaveChangesAsync();
            return pago.IdPago;
        }
    }
}