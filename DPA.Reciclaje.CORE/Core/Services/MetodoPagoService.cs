using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepository _metodoPagoRepository;
        public MetodoPagoService(IMetodoPagoRepository metodoPagoRepository)
        {
            _metodoPagoRepository = metodoPagoRepository;
        }

        public async Task<IEnumerable<MetodoPagoResponseDTO>> GetAllAsync()
        {
            var list = await _metodoPagoRepository.GetAllMetodoPagos();
            return list.Select(m => new MetodoPagoResponseDTO { IdMetodoPago = m.IdMetodoPago, Nombre = m.Nombre });
        }

        public async Task<MetodoPagoResponseDTO?> GetByIdAsync(int id)
        {
            var m = await _metodoPagoRepository.GetMetodoPagoById(id);
            if (m == null) return null;
            return new MetodoPagoResponseDTO { IdMetodoPago = m.IdMetodoPago, Nombre = m.Nombre };
        }

        public async Task<int> CreateAsync(MetodoPagoDTO dto)
        {
            var metodo = new MetodoPago { Nombre = dto.Nombre };
            var id = await _metodoPagoRepository.AddMetodoPago(metodo);
            return id;
        }
    }
}