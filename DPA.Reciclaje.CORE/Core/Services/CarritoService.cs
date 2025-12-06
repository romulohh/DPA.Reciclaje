using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using System.Linq;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class CarritoService : ICarritoService
    {
        private readonly ICarritoRepository _carritoRepository;
        public CarritoService(ICarritoRepository carritoRepository)
        {
            _carritoRepository = carritoRepository;
        }

        public async Task<int> CreateAsync(CarritoDTO dto)
        {
            var carrito = new Carrito
            {
                IdUsuario = dto.IdUsuario,
                Estado = dto.Estado,
                Fecha = DateTime.Now,
                CarritoProducto = dto.Items?.Select(i => new CarritoProducto { IdProducto = (int)i.IdProducto, Precio = i.Precio, Fecha = DateTime.Now }).ToList() ?? new List<CarritoProducto>()
            };

            return await _carritoRepository.AddCarrito(carrito);
        }

        public async Task<bool> UpdateAsync(int id, CarritoDTO dto)
        {
            // Obtener carrito actual con sus productos
            var carritoBD = await _carritoRepository.GetCarritoById(id);
            if (carritoBD == null) return false;

            // Sincronizar productos usando la lógica que te mostré
            return await SyncCarritoProducts(carritoBD, dto.Items?.ToList() ?? new List<CarritoItemDTO>());
        }

        private async Task<bool> SyncCarritoProducts(Carrito carritoBD, List<CarritoItemDTO> nuevosItems)
        {
            // Convertir a diccionarios para comparación eficiente
            var nuevosDict = nuevosItems.ToDictionary(x => x.IdProducto ?? 0, x => x);
            var actualesDict = carritoBD.CarritoProducto.ToDictionary(x => x.IdProducto, x => x);

            var productosAAgregar = new List<CarritoProducto>();
            var productosAActualizar = new List<CarritoProducto>();
            var productosAEliminar = new List<CarritoProducto>();

            // Identificar productos a ELIMINAR (están en BD pero no en request)
            foreach (var productoActual in actualesDict.Values)
            {
                if (!nuevosDict.ContainsKey((int)productoActual.IdProducto))
                {
                    productosAEliminar.Add(productoActual);
                }
            }

            // Identificar productos a AGREGAR o ACTUALIZAR
            foreach (var nuevoItem in nuevosDict.Values)
            {
                if (nuevoItem.IdProducto.HasValue)
                {
                    if (actualesDict.TryGetValue(nuevoItem.IdProducto.Value, out var productoExistente))
                    {
                        // ACTUALIZAR producto existente
                        if (productoExistente.Precio != nuevoItem.Precio)
                        {
                            productoExistente.Precio = nuevoItem.Precio;
                            productoExistente.Fecha = DateTime.Now;
                            productosAActualizar.Add(productoExistente);
                        }
                    }
                    else
                    {
                        // AGREGAR nuevo producto
                        productosAAgregar.Add(new CarritoProducto
                        {
                            IdCarrito = carritoBD.IdCarrito,
                            IdProducto = nuevoItem.IdProducto.Value,
                            Precio = nuevoItem.Precio,
                            Fecha = DateTime.Now
                        });
                    }
                }
            }

            // Aplicar cambios
            return await _carritoRepository.SyncCarritoProductsAsync(
                carritoBD.IdCarrito, productosAAgregar, productosAActualizar, productosAEliminar);
        }

        public async Task<IEnumerable<CarritoResponseDTO>> GetAllAsync()
        {
            var list = await _carritoRepository.GetAllCarritos();
            return list.Select(c => MapToDto(c));
        }

        public async Task<CarritoResponseDTO?> GetByIdAsync(int id)
        {
            var c = await _carritoRepository.GetCarritoById(id);
            if (c == null) return null;
            return MapToDto(c);
        }

        public async Task<IEnumerable<CarritoResponseDTO>> GetByUsuarioAsync(int usuarioId)
        {
            var list = await _carritoRepository.GetCarritosByUsuarioId(usuarioId);
            return list.Select(c => MapToDto(c));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _carritoRepository.DeleteCarrito(id);
        }

        public async Task<int> AddItemAsync(int idCarrito, int idProducto, decimal precio)
        {
            var item = new CarritoProducto
            {
                IdCarrito = idCarrito,
                IdProducto = idProducto,
                Precio = precio,
                Fecha = DateTime.Now
            };

            return await _carritoRepository.AddCarritoProducto(item);
        }

        public async Task<bool> ExistsProductInCarritoAsync(int idCarrito, int idProducto)
        {
            return await _carritoRepository.ExistsProductInCarrito(idCarrito, idProducto);
        }

        private static CarritoResponseDTO MapToDto(Carrito c)
        {
            return new CarritoResponseDTO
            {
                IdCarrito = c.IdCarrito,
                Usuario = c.IdUsuarioNavigation != null ? new UsuarioNestedDTO
                {
                    IdUsuario = c.IdUsuarioNavigation.IdUsuario,
                    Nombres = c.IdUsuarioNavigation.Nombres,
                    Email = c.IdUsuarioNavigation.Email
                } : null,
                Fecha = c.Fecha,
                Estado = c.Estado,
                Items = c.CarritoProducto?.Select(i => new CarritoItemResponseDTO
                {
                    IdCarritoItem = i.IdCarritoItem,
                    Fecha = i.Fecha,
                    Producto = i.IdProductoNavigation != null ? new ProductoNestedDTO
                    {
                        IdProducto = i.IdProductoNavigation.IdProducto,
                        Nombre = i.IdProductoNavigation.Nombre,
                        Estado = i.IdProductoNavigation.Estado,
                        Descripcion = i.IdProductoNavigation.Descripcion,
                        Imagen = i.IdProductoNavigation.ProductoImg != null && i.IdProductoNavigation.ProductoImg.Any() ? i.IdProductoNavigation.ProductoImg.First().Imagen : null,
                        Precio = i.Precio ?? i.IdProductoNavigation.Precio
                    } : null
                })
            };
        }
    }
}
