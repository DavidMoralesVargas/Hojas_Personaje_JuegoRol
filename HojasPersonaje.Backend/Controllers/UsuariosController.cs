using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace HojasPersonaje.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultado = await _usuarioService.ObtenerTodos();
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(Usuario usuario)
        {
            var resultado = await _usuarioService.Guardar(usuario);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPut]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            var resultado = await _usuarioService.Editar(usuario);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _usuarioService.Eliminar(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var resultado = await _usuarioService.ObtenerPorId(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }
    }
}
