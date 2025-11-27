using DPA.Reciclaje.CORE.Core.Entities;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Reciclaje.CORE.Infrastructure.Repositories
{
    public class CampaniaRepository : ICampaniaRepository
    {
        private readonly ReciclaDbContext _context;
        public CampaniaRepository(ReciclaDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Campania>> GetAllCampanias()
        {
            return await _context.Campania.ToListAsync();
        }
        public async Task<Campania?> GetCampaniaById(int id)
        {
            return await _context.Campania.Where(c => c.IdCampania == id).FirstOrDefaultAsync();
        }
        public async Task<int> AddCampania(Campania campania)
        {
            await _context.Campania.AddAsync(campania);
            await _context.SaveChangesAsync();
            return campania.IdCampania;
        }

        public async Task<bool> UpdateCampania(Campania campania)
        {
            var existing = await _context.Campania.FindAsync(campania.IdCampania);
            if (existing == null) return false;

            existing.Título = campania.Título;
            existing.Descripcion = campania.Descripcion;
            existing.FechaInicio = campania.FechaInicio;
            existing.FechaFin = campania.FechaFin;
            existing.IdDistrito = campania.IdDistrito;
            existing.IdUsuario = campania.IdUsuario;

            _context.Campania.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCampania(int id)
        {
            var existing = await _context.Campania.FindAsync(id);
            if (existing == null) return false;
            _context.Campania.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
