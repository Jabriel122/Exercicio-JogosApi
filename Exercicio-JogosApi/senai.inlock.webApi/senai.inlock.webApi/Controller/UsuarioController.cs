using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interface;
using senai.inlock.webApi_.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai.inlock.webApi_.Controller
{
    [Route("api/[controller]")]

    [ApiController]

    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {

        private IUsauarioRepository _usaurioRepository;

        public UsuarioController()
        {
            _usaurioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Endpoint para Logar
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(UsuarioDomain usuario)
        {
            try
            {
                UsuarioDomain usuarioBuscado = _usaurioRepository.Logar(usuario.Email, usuario.Senha);

                if (usuarioBuscado == null)
                {
                    {
                        return NotFound("Email ou Senha Inválido");
                    }
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("inlock-chave-autenticacao-manha-dev-api"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                    (
                        issuer: "senai.inlock.webApi",

                        audience: "senai.inlock.webApi",

                        claims: claims,

                        expires: DateTime.Now.AddMinutes(5),

                        signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                });

            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        } 
    }
}
