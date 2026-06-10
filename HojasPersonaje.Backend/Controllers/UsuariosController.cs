using HojasPersonaje.Backend.DTOs;
using HojasPersonaje.Backend.Entidades.Usuarios;
using HojasPersonaje.Backend.Services.Interfaces.Usuarios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HojasPersonaje.Backend.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public UsuariosController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
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
        public async Task<IActionResult> Ingresar([FromBody] UsuarioDTO usuario)
        {
            var resultado = await _usuarioService.Ingresar(usuario);
            if (resultado.Exitoso)
            {
                return Ok(ConstruirToken(resultado.Resultado!));
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

        //Método para crear el token con claims
        private TokenDTO ConstruirToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, usuario.Nombre_Usuario!),
                new(ClaimTypes.Role, usuario.tipoUsuario.ToString()),
                new("sub", usuario.Nombre_Usuario!),
                new("Foto", usuario.Foto ?? string.Empty)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(30);
            var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);
            return new TokenDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiration
            };
        }
    }
}
