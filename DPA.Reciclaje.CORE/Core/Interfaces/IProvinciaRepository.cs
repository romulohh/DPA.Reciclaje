using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IProvinciaRepository
    {
        Task<IEnumerable<Provincia>> GetAllProvincias();
        Task<Provincia?> GetProvinciaById(int id);
        Task<int> AddProvincia(Provincia provincia);
        Task<IEnumerable<Provincia>> GetProvinciasByDepartamento(int departamentoId);
    }
}
