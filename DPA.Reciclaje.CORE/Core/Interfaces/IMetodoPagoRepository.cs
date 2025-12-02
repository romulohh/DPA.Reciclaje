using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IMetodoPagoRepository
    {
        Task<IEnumerable<MetodoPago>> GetAllMetodoPagos();
        Task<MetodoPago?> GetMetodoPagoById(int id);
        Task<int> AddMetodoPago(MetodoPago metodoPago);
    }
}