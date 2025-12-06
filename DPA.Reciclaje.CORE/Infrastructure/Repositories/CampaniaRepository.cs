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

        public async Task<IEnumerable<Campania>> GetCampaniasVigentes()
        {
            var now = DateTime.Now;
            return await _context.Campania
                .Where(c => c.FechaInicio <= now && c.FechaFin >= now)
                .ToListAsync();
        }
    }
}
