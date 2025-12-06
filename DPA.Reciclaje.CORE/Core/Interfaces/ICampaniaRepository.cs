using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface ICampaniaRepository
    {
        Task<int> AddCampania(Campania campania);
        Task<IEnumerable<Campania>> GetAllCampanias();
        Task<Campania?> GetCampaniaById(int id);
        Task<IEnumerable<Campania>> GetCampaniasVigentes();
    }
}