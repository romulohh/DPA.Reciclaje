using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CampaniaController : ControllerBase
    {
        private readonly ICampaniaService _campaniaService;
        public CampaniaController(ICampaniaService campaniaService)
        {
            _campaniaService = campaniaService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var list = await _campaniaService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var campania = await _campaniaService.GetByIdAsync(id);
            if (campania == null) return NotFound();
            return Ok(campania);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CampaniaDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Título))
                return BadRequest("Título es obligatorio.");

            var id = await _campaniaService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear la Campaña.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CampaniaDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Título))
                return BadRequest("Título es obligatorio.");

            var updated = await _campaniaService.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _campaniaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
