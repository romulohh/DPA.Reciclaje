using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _categoriaService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _categoriaService.GetByIdAsync(id);
            if (cat == null) return NotFound();
            return Ok(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _categoriaService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear la categoría.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}
