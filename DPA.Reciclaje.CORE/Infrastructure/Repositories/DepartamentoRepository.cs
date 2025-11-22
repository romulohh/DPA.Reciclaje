using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class DepartamentoRepository : IDepartamentoRepository
    {
        private readonly ReciclaDbContext _context;
        public DepartamentoRepository(ReciclaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Departamento>> GetAllDepartamentos()
        {
            return await _context.Departamento.ToListAsync();
        }

        public async Task<Departamento?> GetDepartamentoById(int id)
        {
            return await _context.Departamento.Where(d => d.IdDepartamento == id).FirstOrDefaultAsync();
        }

        public async Task<int> AddDepartamento(Departamento departamento)
        {
            await _context.Departamento.AddAsync(departamento);
            await _context.SaveChangesAsync();
            return departamento.IdDepartamento;
        }
    }
}
