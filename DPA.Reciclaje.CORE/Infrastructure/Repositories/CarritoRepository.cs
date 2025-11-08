using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class CarritoRepository : ICarritoRepository
    {
        private readonly ReciclaDbContext _context;
        public CarritoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddCarrito(Carrito carrito)
        {
            await _context.Carrito.AddAsync(carrito);
            await _context.SaveChangesAsync();
            return carrito.IdCarrito;
        }

        public async Task<bool> SyncCarritoProductsAsync(int carritoId,
        List<CarritoProducto> productosAAgregar,
        List<CarritoProducto> productosAActualizar,
        List<CarritoProducto> productosAEliminar)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Eliminar productos
                if (productosAEliminar.Any())
                {
                    _context.CarritoProducto.RemoveRange(productosAEliminar);
                }

                // Actualizar productos
                if (productosAActualizar.Any())
                {
                    _context.CarritoProducto.UpdateRange(productosAActualizar);
                }

                // Agregar nuevos productos
                if (productosAAgregar.Any())
                {
                    await _context.CarritoProducto.AddRangeAsync(productosAAgregar);
                }

                // Actualizar fecha del carrito
                var carrito = await _context.Carrito.FindAsync(carritoId);
                if (carrito != null)
                {
                    carrito.Fecha = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdateCarrito(Carrito carrito)
        {
            // Load existing carrito with items
            var existing = await _context.Carrito
                .Include(c => c.CarritoProducto)
                .FirstOrDefaultAsync(c => c.IdCarrito == carrito.IdCarrito);

            if (existing == null) return false;

            existing.Estado = carrito.Estado;
            existing.IdUsuario = carrito.IdUsuario;

            // If incoming items is null, remove all items
            var incomingItems = carrito.CarritoProducto?.Where(i => i.IdProducto.HasValue).ToList() ?? new List<CarritoProducto>();

            // Remove items that are not in incoming (compare by IdProducto)
            var incomingProductIds = new HashSet<int>(incomingItems.Where(i => i.IdProducto.HasValue).Select(i => i.IdProducto!.Value));

            var toRemove = existing.CarritoProducto.Where(ei => !ei.IdProducto.HasValue || !incomingProductIds.Contains(ei.IdProducto.Value)).ToList();
            if (toRemove.Any())
            {
                _context.CarritoProducto.RemoveRange(toRemove);
            }

            // Update existing items or add new ones
            foreach (var inc in incomingItems)
            {
                if (!inc.IdProducto.HasValue) continue;
                var prodId = inc.IdProducto.Value;
                var existingItem = existing.CarritoProducto.FirstOrDefault(ei => ei.IdProducto == prodId);
                if (existingItem != null)
                {
                    // update price and date if provided
                    if (inc.Precio.HasValue) existingItem.Precio = inc.Precio;
                    existingItem.Fecha = DateTime.Now;
                    _context.CarritoProducto.Update(existingItem);
                }
                else
                {
                    var newItem = new CarritoProducto
                    {
                        IdCarrito = existing.IdCarrito,
                        IdProducto = prodId,
                        Precio = inc.Precio,
                        Fecha = DateTime.Now
                    };
                    await _context.CarritoProducto.AddAsync(newItem);
                }
            }

            // Save changes
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCarrito(int id)
        {
            var existing = await _context.Carrito.FindAsync(id);
            if (existing == null) return false;
            _context.Carrito.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Carrito>> GetAllCarritos()
        {
            return await _context.Carrito
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.CarritoProducto)
                    .ThenInclude(ci => ci.IdProductoNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Carrito?> GetCarritoById(int id)
        {
            return await _context.Carrito
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.CarritoProducto)
                    .ThenInclude(ci => ci.IdProductoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCarrito == id);
        }

        public async Task<IEnumerable<Carrito>> GetCarritosByUsuarioId(int usuarioId)
        {
            return await _context.Carrito
                .Include(c => c.IdUsuarioNavigation)
                .Include(c => c.CarritoProducto)
                    .ThenInclude(ci => ci.IdProductoNavigation)
                .Where(c => c.IdUsuario == usuarioId)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
