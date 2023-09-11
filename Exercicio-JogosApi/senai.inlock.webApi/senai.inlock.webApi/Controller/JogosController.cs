using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi_.Domains;
using senai.inlock.webApi_.Interface;
using senai.inlock.webApi_.Repositories;
using System.Data;

namespace senai.inlock.webApi_.Controller
{

    [Route("api/[controller]")]

    [ApiController]

    [Produces("application/json")]

   

    public class JogosController : ControllerBase
    {


        private IJogosRepository _jogosRepository;

        public JogosController()
        {
            _jogosRepository = new JogosRepository();

        }

        /// <summary>
        /// Endpoint para listagem de jogos
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //Cria uma lista que recebe os dados da requisição
                List<JogosDomains> ListaJogo = _jogosRepository.ListarTodos();

                //Retorna a lista no formato JSON com o status code Ok(200)  
                return Ok(ListaJogo);
            }
            catch (Exception erro)
            {
                //Retorna um status code BadRequest(400) e a mensagem do erro
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public IActionResult Post(JogosDomains jogoNovo)
        {
            try
            {
                _jogosRepository.Cadastre(jogoNovo);

                return StatusCode(201);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }


    }
}
