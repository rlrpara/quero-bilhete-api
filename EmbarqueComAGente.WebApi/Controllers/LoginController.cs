using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace QueroBilhete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class LoginController : ControllerBase
    {
        #region Propriedades Privadas
        private readonly IUsuarioService _service;
        #endregion

        #region Construtor
        public LoginController(IUsuarioService Service) => _service = Service;
        #endregion

        #region Métodos Públicos
        //[HttpPost("authenticate"), AllowAnonymous]
        //public IActionResult Authenticate([FromBody] UserAuthenticateRequestViewModel entidade)
        //    => Ok(_service.Authenticate(entidade));

        /// <summary>
        /// Autenticação no Firebase
        /// </summary>
        /// /// <remarks>
        /// Examplo envio:
        ///
        ///     POST /api/usuario/authenticatefirebase
        ///     {
        ///        "nomeUsuario": "",
        ///        "email": "jail@gmail.com",
        ///        "anonimo": false,
        ///        "telefoneCelular": "",
        ///        "fotoURL": "",
        ///        "uid": "token-aqui"
        ///     }
        /// </remarks>
        /// <param name="entidade">Classe contendo os dados de login</param>
        /// <returns>ados de autenticação do usuário na base</returns>
        /// <response code="200">Retorna com sucesso na requisição</response>
        /// <response code="400">Retorna com erro ao enviar os dados da entidade, dados ausentes</response>
        /// <response code="401">Retorna com erro ao enviar token inválido ou expirado</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost("authenticatefirebase")]
        public IActionResult PostCheckFirebase([FromBody] FirebaseAuthenticate entidade)
        {
            if (ModelState.IsValid)
                return Ok(_service.AuthenticateFirebaseCheck(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        /// <summary>
        /// Retornar se o usuário existe no banco de dados com base no UID
        /// </summary>
        /// <param name="uid">Informe aqui o código UID</param>
        /// <response code="200">Retorna com sucesso na requisição</response>
        /// <response code="400">Retorna quando não informado o UID</response>
        /// <response code="404">Retorna como dados não encontrados</response>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("validausuario/{uid}")]
        public IActionResult PostValidarUsuario(string uid)
        {
            if (string.IsNullOrEmpty(uid))
                BadRequest($"Indentificador inválido");
            else
            {
                if (_service.ValidaUidUsuario(uid))
                    return Ok(new { Resultado = "OK." });
                else
                    return NotFound(new { Resultado = $"Código '{uid}' não encontrado." });
            }

            return NotFound();
        }
        #endregion
    }
}
