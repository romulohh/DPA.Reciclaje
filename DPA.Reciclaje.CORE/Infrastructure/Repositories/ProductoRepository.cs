using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly ReciclaDbContext _context;
        public ProductoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> GetAllProductos()
        {
            return await _context.Producto
                // Agregado para publicar imagen principal
                .Include(p => p.ProductoImg)
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdDepartamentoNavigation)
                            .ThenInclude(dep => dep.IdPaisNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdProvinciaNavigation)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Producto?> GetProductoById(int id)
        {
            return await _context.Producto.Where(p => p.IdProducto == id)
                // Agregado para publicar imagen principal
                .Include(p => p.ProductoImg)
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdDepartamentoNavigation)
                            .ThenInclude(dep => dep.IdPaisNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdProvinciaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddProducto(Producto producto)
        {
            await _context.Producto.AddAsync(producto);
            await _context.SaveChangesAsync();
            return producto.IdProducto;
        }

        // NUEVO
        public async Task AddImagenesAsync(int idProducto, IEnumerable<string> nombresImagenes)
        {
            foreach (var nombre in nombresImagenes)
            {
                var img = new ProductoImg
                {
                    IdProducto = idProducto,
                    Imagen = nombre
                };

                await _context.ProductoImg.AddAsync(img);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producto>> GetProductosByFilter(int? categoriaId, int? departamentoId, int? provinciaId, int? distritoId)
        {
            var query = _context.Producto
                // Agregado para publicar imagen principal
                .Include(p => p.ProductoImg)
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdDepartamentoNavigation)
                            .ThenInclude(dep => dep.IdPaisNavigation)
                .Include(p => p.IdUsuarioNavigation)
                    .ThenInclude(u => u.IdDistritoNavigation)
                        .ThenInclude(d => d.IdProvinciaNavigation)
                .AsQueryable();

            if (categoriaId.HasValue)
                query = query.Where(p => p.IdCategoria == categoriaId.Value);

            if (distritoId.HasValue)
                query = query.Where(p => p.IdUsuarioNavigation != null && p.IdUsuarioNavigation.IdDistrito == distritoId.Value);

            if (provinciaId.HasValue)
                query = query.Where(p => p.IdUsuarioNavigation != null && p.IdUsuarioNavigation.IdDistritoNavigation != null && p.IdUsuarioNavigation.IdDistritoNavigation.IdProvincia == provinciaId.Value);

            if (departamentoId.HasValue)
                query = query.Where(p => p.IdUsuarioNavigation != null && p.IdUsuarioNavigation.IdDistritoNavigation != null && p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamento == departamentoId.Value);

            return await query.AsNoTracking().ToListAsync();
        }
    }
}
