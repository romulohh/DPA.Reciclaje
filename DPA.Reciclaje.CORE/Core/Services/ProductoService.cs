using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using System.Linq;
using System;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoResponseDTO>> GetAllAsync()
        {
            var list = await _productoRepository.GetAllProductos();
            return list.Select(p => MapToDto(p));
        }

        public async Task<ProductoResponseDTO?> GetByIdAsync(int id)
        {
            var p = await _productoRepository.GetProductoById(id);
            if (p == null) return null;
            return MapToDto(p);
        }

        public async Task<int> CreateAsync(ProductoDTO dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                Marca = dto.Marca,
                IdCategoria = dto.IdCategoria,
                Motivo = dto.Motivo,
                Estado = dto.Estado,
                Disponible = dto.Disponible,
                Precio = dto.Precio,
                IdUsuario = dto.IdUsuario,
                FechaPublicación = DateTime.Now
            };

            var id = await _productoRepository.AddProducto(producto);
            return id;
        }

        // NUEVO: delega al repositorio
        public async Task AddImagenesAsync(int idProducto, IEnumerable<string> nombresImagenes)
        {
            await _productoRepository.AddImagenesAsync(idProducto, nombresImagenes);
        }

        public async Task<IEnumerable<ProductoResponseDTO>> FilterAsync(int? categoriaId, int? departamentoId, int? provinciaId, int? distritoId)
        {
            var list = await _productoRepository.GetProductosByFilter(categoriaId, departamentoId, provinciaId, distritoId);
            return list.Select(p => MapToDto(p));
        }

        // NUEVO: actualizar producto
        public async Task<bool> UpdateAsync(int id, ProductoDTO dto)
        {
            var existing = await _productoRepository.GetProductoById(id);
            if (existing == null) return false;

            existing.Nombre = dto.Nombre;
            existing.Descripcion = dto.Descripcion;
            existing.Marca = dto.Marca;
            existing.IdCategoria = dto.IdCategoria;
            existing.Motivo = dto.Motivo;
            existing.Estado = dto.Estado;
            existing.Disponible = dto.Disponible;
            existing.Precio = dto.Precio;
            existing.IdUsuario = dto.IdUsuario;

            return await _productoRepository.UpdateProducto(existing);
        }

        // NUEVO: eliminar producto
        public async Task<bool> DeleteAsync(int id)
        {
            return await _productoRepository.DeleteProducto(id);
        }

        private static ProductoResponseDTO MapToDto(Producto p)
        {
            return new ProductoResponseDTO
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Marca = p.Marca,
                Motivo = p.Motivo,
                Estado = p.Estado,
                Disponible = p.Disponible,
                Precio = p.Precio,
                FechaPublicacion = p.FechaPublicación,
                // Agregado para publicar imagen principal
                Imagen = p.ProductoImg != null && p.ProductoImg.Any() ? p.ProductoImg.First().Imagen : null,
                Categoria = p.IdCategoriaNavigation != null ? new CategoriaResponseDTO { IdCategoria = p.IdCategoriaNavigation.IdCategoria, Nombre = p.IdCategoriaNavigation.Nombre } : null,
                Usuario = p.IdUsuarioNavigation != null ? new UsuarioNestedDTO
                {
                    IdUsuario = p.IdUsuarioNavigation.IdUsuario,
                    Nombres = p.IdUsuarioNavigation.Nombres,
                    Email = p.IdUsuarioNavigation.Email,
                    Distrito = p.IdUsuarioNavigation.IdDistritoNavigation != null ? new DistritoNestedDTO
                    {
                        IdDistrito = p.IdUsuarioNavigation.IdDistritoNavigation.IdDistrito,
                        Nombre = p.IdUsuarioNavigation.IdDistritoNavigation.Nombre,
                        Provincia = p.IdUsuarioNavigation.IdDistritoNavigation.IdProvinciaNavigation != null ? new ProvinciaNestedDTO
                        {
                            IdProvincia = p.IdUsuarioNavigation.IdDistritoNavigation.IdProvinciaNavigation.IdProvincia,
                            Nombre = p.IdUsuarioNavigation.IdDistritoNavigation.IdProvinciaNavigation.Nombre
                        } : null,
                        Departamento = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation != null ? new DepartamentoNestedDTO
                        {
                            IdDepartamento = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation.Nombre,
                            Pais = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation.IdPaisNavigation != null ? new PaisNestedDTO
                            {
                                IdPais = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation.IdPaisNavigation.IdPais,
                                Nombre = p.IdUsuarioNavigation.IdDistritoNavigation.IdDepartamentoNavigation.IdPaisNavigation.Nombre
                            } : null
                        } : null
                    } : null
                } : null
            };
        }
    }
}
