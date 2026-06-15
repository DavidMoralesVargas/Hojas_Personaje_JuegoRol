using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Entidades.Vampiros;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Vampiros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HojasPersonaje.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class HabilidadDisciplinaController : ControllerBase
    {
        private readonly IHabilidadDisciplinaServices _services;
        private readonly IUsuarioService _userService;

        public HabilidadDisciplinaController(IHabilidadDisciplinaServices services, IUsuarioService userService)
        {
            _services = services;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());

            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.ObtenerTodos();
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet("all/{id}")]
        public async Task<IActionResult> ObtenerTodosPorId(int id)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.ObtenerTodosPorId(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.ObtenerPorId(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] HabilidadDisciplina habilidadDisciplina)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.Guardar(habilidadDisciplina);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPost("all")]
        public async Task<IActionResult> GuardarTodos([FromBody] List<HabilidadDisciplina> habilidades)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.GuardarTodos(habilidades);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPut]
        public async Task<IActionResult> Editar(HabilidadDisciplina habilidadDisciplina)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.Editar(habilidadDisciplina);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.Eliminar(id);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }
    }
}
