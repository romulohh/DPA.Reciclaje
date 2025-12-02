using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly IMetodoPagoService _metodoPagoService;
        public MetodoPagoController(IMetodoPagoService metodoPagoService)
        {
            _metodoPagoService = metodoPagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _metodoPagoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var mp = await _metodoPagoService.GetByIdAsync(id);
            if (mp == null) return NotFound();
            return Ok(mp);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MetodoPagoDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Nombre))
                return BadRequest("Nombre es obligatorio.");

            var id = await _metodoPagoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Metodo de Pago.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}