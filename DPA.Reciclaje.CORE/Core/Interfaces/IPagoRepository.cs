using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IPagoRepository
    {
        Task<IEnumerable<Pago>> GetAllPagos();
        Task<Pago?> GetPagoById(int id);
        Task<int> AddPago(Pago pago);
    }
}