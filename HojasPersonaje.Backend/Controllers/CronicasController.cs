using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Hojas;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HojasPersonaje.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CronicasController : ControllerBase
    {
        private readonly ICronicasServices _cronicasServices;
        private readonly IUsuarioService _userService;


        public CronicasController(ICronicasServices cronicasServices, IUsuarioService usuarioService)
        {
            _cronicasServices = cronicasServices;
            _userService = usuarioService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> ObtenerTodosPorId()
        {
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;

            var resultado = await _cronicasServices.ObtenerTodosPorId(nameClaim!);
            if(!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<IActionResult> ObtenerPorCodigo(string codigo)
        {
            var resultado = await _cronicasServices.ObtenerPorCodigo(codigo);
            if (!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpGet("porid/{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _cronicasServices.ObtenerPorId(id);
            if (!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }


        [HttpPost]
        public async Task<IActionResult> Guardar([FromBody] CronicaDTO cronica)
        {
            var nameClaim = User.FindFirst(ClaimTypes.Name)?.Value;


            var resultado = await _cronicasServices.Guardar(cronica, nameClaim!);
            if (!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] CronicaDTO cronica)
        {

            var resultado = await _cronicasServices.Editar(cronica);
            if (!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _cronicasServices.Eliminar(id);
            if (!resultado.Exitoso)
            {
                return Ok(resultado.Resultado);
            }
            return BadRequest(resultado.Mensaje);
        }
    }
}
