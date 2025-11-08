using DPA.Reciclaje.CORE.Core.DTOs;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<PagoResponseDTO>> GetAllAsync();
        Task<PagoResponseDTO?> GetByIdAsync(int id);
        Task<int> CreateAsync(PagoDTO dto);
    }
}