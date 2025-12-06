using DPA.Reciclaje.CORE.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> GetAllProductos();
        Task<Producto?> GetProductoById(int id);
        Task<int> AddProducto(Producto producto);
        Task<IEnumerable<Producto>> GetProductosByFilter(int? categoriaId, int? departamentoId, int? provinciaId, int? distritoId);

        // NUEVO: registrar las imágenes de un producto
        Task AddImagenesAsync(int idProducto, IEnumerable<string> nombresImagenes);

        // NUEVO: actualizar y eliminar producto
        Task<bool> UpdateProducto(Producto producto);
        Task<bool> DeleteProducto(int id);
    }
}
