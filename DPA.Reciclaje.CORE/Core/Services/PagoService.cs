using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using System.Linq;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public async Task<IEnumerable<PagoResponseDTO>> GetAllAsync()
        {
            var list = await _pagoRepository.GetAllPagos();
            return list.Select(p => new PagoResponseDTO
            {
                IdPago = p.IdPago,
                Carrito = new CarritoResponseDTO
                {
                    IdCarrito = p.IdCarritoNavigation?.IdCarrito ?? 0,
                    Items = p.IdCarritoNavigation?.CarritoProducto.Select(ci => new CarritoItemResponseDTO
                    {
                        IdCarritoItem = ci.IdCarritoItem,
                        Fecha = ci.Fecha,
                        Producto = ci.IdProductoNavigation == null ? null : new ProductoNestedDTO
                        {
                            IdProducto = ci.IdProductoNavigation.IdProducto,
                            Nombre = ci.IdProductoNavigation.Nombre,
                            Estado = ci.IdProductoNavigation.Estado,
                            Precio = ci.Precio
                        }
                    })
                },
                MetodoPago = p.IdMetodoPagoNavigation == null ? null : new MetodoPagoResponseDTO
                {
                    IdMetodoPago = p.IdMetodoPagoNavigation.IdMetodoPago,
                    Nombre = p.IdMetodoPagoNavigation.Nombre
                },
                Monto = p.Monto,
                Fecha = p.Fecha,
                NumeroOperacion = p.NumeroOperacion ?? string.Empty
            });
        }

        public async Task<PagoResponseDTO?> GetByIdAsync(int id)
        {
            var p = await _pagoRepository.GetPagoById(id);
            if (p == null) return null;

            return new PagoResponseDTO
            {
                IdPago = p.IdPago,
                Carrito = new CarritoResponseDTO
                {
                    IdCarrito = p.IdCarritoNavigation?.IdCarrito ?? 0,
                    Items = p.IdCarritoNavigation?.CarritoProducto.Select(ci => new CarritoItemResponseDTO
                    {
                        IdCarritoItem = ci.IdCarritoItem,
                        Fecha = ci.Fecha,
                        Producto = ci.IdProductoNavigation == null ? null : new ProductoNestedDTO
                        {
                            IdProducto = ci.IdProductoNavigation.IdProducto,
                            Nombre = ci.IdProductoNavigation.Nombre,
                            Estado = ci.IdProductoNavigation.Estado,
                            Precio = ci.Precio
                        }
                    })
                },
                MetodoPago = p.IdMetodoPagoNavigation == null ? null : new MetodoPagoResponseDTO
                {
                    IdMetodoPago = p.IdMetodoPagoNavigation.IdMetodoPago,
                    Nombre = p.IdMetodoPagoNavigation.Nombre
                },
                Monto = p.Monto,
                Fecha = p.Fecha,
                NumeroOperacion = p.NumeroOperacion ?? string.Empty
            };
        }

        public async Task<int> CreateAsync(PagoDTO dto)
        {
            var pago = new Pago
            {
                IdCarrito = dto.IdCarrito,
                IdMetodoPago = dto.IdMetodoPago,
                Monto = dto.Monto,
                NumeroOperacion = string.IsNullOrWhiteSpace(dto.NumeroOperacion) ? null : dto.NumeroOperacion,
                Fecha = DateTime.Now
            };
            var id = await _pagoRepository.AddPago(pago);
            return id;
        }
    }
}