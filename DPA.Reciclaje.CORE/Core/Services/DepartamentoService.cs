using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        public DepartamentoService(IDepartamentoRepository departamentoRepository)
        {
            _departamentoRepository = departamentoRepository;
        }

        public async Task<IEnumerable<DepartamentoResponseDTO>> GetAllAsync()
        {
            var list = await _departamentoRepository.GetAllDepartamentos();
            return list.Select(d => new DepartamentoResponseDTO { IdDepartamento = d.IdDepartamento, Nombre = d.Nombre });
        }

        public async Task<DepartamentoResponseDTO?> GetByIdAsync(int id)
        {
            var d = await _departamentoRepository.GetDepartamentoById(id);
            if (d == null) return null;
            return new DepartamentoResponseDTO { IdDepartamento = d.IdDepartamento, Nombre = d.Nombre };
        }

        public async Task<int> CreateAsync(DepartamentoDTO dto)
        {
            var departamento = new Departamento { Nombre = dto.Nombre, IdPais = dto.IdPais };
            var id = await _departamentoRepository.AddDepartamento(departamento);
            return id;
        }
    }
}
