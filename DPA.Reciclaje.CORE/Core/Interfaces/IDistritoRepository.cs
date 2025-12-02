using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IDistritoRepository
    {
        Task<IEnumerable<Distrito>> GetAllDistritos();
        Task<Distrito?> GetDistritoById(int id);
        Task<int> AddDistrito(Distrito distrito);
        Task<IEnumerable<Distrito>> GetDistritosByProvincia(int provinciaId);
        Task<IEnumerable<Distrito>> GetDistritosByDepartamento(int departamentoId);
    }
}
