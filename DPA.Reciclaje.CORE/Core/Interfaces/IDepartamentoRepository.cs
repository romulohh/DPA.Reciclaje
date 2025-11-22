using DPA.Reciclaje.CORE.Core.Entities;

namespace DPA.Reciclaje.CORE.Core.Interfaces
{
    public interface IDepartamentoRepository
    {
        Task<IEnumerable<Departamento>> GetAllDepartamentos();
        Task<Departamento?> GetDepartamentoById(int id);
        Task<int> AddDepartamento(Departamento departamento);
    }
}
