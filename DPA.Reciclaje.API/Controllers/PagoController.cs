using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;
        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _pagoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pago = await _pagoService.GetByIdAsync(id);
            if (pago == null) return NotFound();
            return Ok(pago);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PagoDTO dto)
        {
            if (dto == null || dto.Monto <= 0)
                return BadRequest("Monto es obligatorio y mayor a 0.");

            var id = await _pagoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Pago.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}