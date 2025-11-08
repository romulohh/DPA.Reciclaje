using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IMetodoPagoService
    {
        Task<IEnumerable<MetodoPagoResponseDTO>> GetAllAsync();
        Task<MetodoPagoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(MetodoPagoDTO dto);
    }
}