using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;

namespace DPA.Reciclaje.CORE.Core.Services
{
    public class ProvinciaService : IProvinciaService
    {
        private readonly IProvinciaRepository _provinciaRepository;
        public ProvinciaService(IProvinciaRepository provinciaRepository)
        {
            _provinciaRepository = provinciaRepository;
        }

        public async Task<IEnumerable<ProvinciaResponseDTO>> GetAllAsync()
        {
            var list = await _provinciaRepository.GetAllProvincias();
            return list.Select(p => new ProvinciaResponseDTO { IdProvincia = p.IdProvincia, IdDepartamento = p.IdDepartamento, Nombre = p.Nombre });
        }

        public async Task<ProvinciaResponseDTO?> GetByIdAsync(int id)
        {
            var p = await _provinciaRepository.GetProvinciaById(id);
            if (p == null) return null;
            return new ProvinciaResponseDTO { IdProvincia = p.IdProvincia, IdDepartamento = p.IdDepartamento, Nombre = p.Nombre };
        }

        public async Task<int> CreateAsync(ProvinciaDTO dto)
        {
            var provincia = new Provincia { Nombre = dto.Nombre, IdDepartamento = dto.IdDepartamento };
            var id = await _provinciaRepository.AddProvincia(provincia);
            return id;
        }

        public async Task<IEnumerable<ProvinciaResponseDTO>> GetByDepartamentoAsync(int departamentoId)
        {
            var list = await _provinciaRepository.GetProvinciasByDepartamento(departamentoId);
            return list.Select(p => new ProvinciaResponseDTO { IdProvincia = p.IdProvincia, IdDepartamento = p.IdDepartamento, Nombre = p.Nombre });
        }
    }
}
