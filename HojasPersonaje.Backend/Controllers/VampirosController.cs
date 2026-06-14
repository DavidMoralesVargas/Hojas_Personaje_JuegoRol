using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Usuarios;
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
    public class VampirosController : ControllerBase
    {
        private readonly IVampirosServices _services;
        private readonly IUsuarioService _userService;

        public VampirosController(IVampirosServices services, IUsuarioService userService)
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
        public async Task<IActionResult> Crear([FromBody] VampiroDTO vampiro)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.Guardar(vampiro);
            if (resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] VampiroDTO vampiro)
        {
            //Verifica que el usuario ingresado tiene el rol de administrador
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;
            var hasRol = await _userService.VerificarUsuarioRol(nameClaim!, TipoUsuario.Administrador.ToString());
            if (!hasRol.Exitoso)
            {
                return StatusCode(StatusCodes.Status403Forbidden, hasRol.Mensaje);
            }

            var resultado = await _services.Editar(vampiro);
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
