using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritoController : ControllerBase
    {
        private readonly IFavoritoService _favoritoService;
        public FavoritoController(IFavoritoService favoritoService)
        {
            _favoritoService = favoritoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _favoritoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fav = await _favoritoService.GetByIdAsync(id);
            if (fav == null) return NotFound();
            return Ok(fav);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoritoDTO dto)
        {
            if (dto == null || dto.IdProducto <= 0 || dto.IdUsuario <= 0)
                return BadRequest("IdUsuario e IdProducto son obligatorios.");

            var id = await _favoritoService.CreateAsync(dto);
            if (id == 0) return Conflict("No se pudo crear el Favorito.");

            return CreatedAtAction(nameof(GetById), new { id }, dto);
        }
    }
}
