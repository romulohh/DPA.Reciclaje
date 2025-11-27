using DPA.Reciclaje.CORE.Core.DTOs;
using DPA.Reciclaje.CORE.Core.Interfaces;
using DPA.Reciclaje.CORE.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Reciclaje.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService UsuarioService)
        {
            _usuarioService = UsuarioService;
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Clave))
                return BadRequest("Email y Clave son obligatorios.");

            var user = await _usuarioService.SignInAsync(dto);
            if (user == null) return Unauthorized();

            return Ok(user);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Clave) || string.IsNullOrWhiteSpace(dto.Nombres))
                return BadRequest("Nombre, Email y Clave son obligatorios.");

            var id = await _usuarioService.SignUpAsync(dto);
            if (id == 0) return Conflict("El email ya está registrado.");

            return CreatedAtAction(null, new { id }, dto);
        }

        [HttpGet("exists")]
        public async Task<IActionResult> ExistsByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return BadRequest("Email es obligatorio.");

            var exists = await _usuarioService.ExistsByEmailAsync(email);
            return Ok(exists);
        }
    }
    }
