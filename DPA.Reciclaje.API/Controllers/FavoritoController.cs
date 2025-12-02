using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] int? idProducto, [FromQuery] int? idUsuario)
        {
            var list = await _favoritoService.GetAllAsync();
            var query = list.AsQueryable();

            if (idProducto.HasValue)
                query = query.Where(f => f.IdProducto == idProducto.Value);
            if (idUsuario.HasValue)
                query = query.Where(f => f.IdUsuario == idUsuario.Value);

            return Ok(query);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteByProductoUsuario([FromQuery] int idProducto, [FromQuery] int idUsuario)
        {
            if (idProducto <= 0 || idUsuario <= 0) return BadRequest("idProducto e idUsuario son obligatorios.");

            var deleted = await _favoritoService.DeleteByProductoUsuarioAsync(idProducto, idUsuario);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
