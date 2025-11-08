using DPA.Reciclaje.CORE.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICarritoRepository
    {
        Task<int> AddCarrito(Carrito carrito);
        Task<bool> SyncCarritoProductsAsync(int carritoId,
        List<CarritoProducto> productosAAgregar,
        List<CarritoProducto> productosAActualizar,
        List<CarritoProducto> productosAEliminar);
        Task<bool> UpdateCarrito(Carrito carrito);
        Task<IEnumerable<Carrito>> GetAllCarritos();
        Task<Carrito?> GetCarritoById(int id);
        Task<IEnumerable<Carrito>> GetCarritosByUsuarioId(int usuarioId);
        Task<bool> DeleteCarrito(int id);
    }
}
