using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System.Linq;

namespace QueroBilhete.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EmbarcacaoController : ControllerBase
    {
        private readonly IEmbarcacaoService _service;

        public EmbarcacaoController(IEmbarcacaoService service)
            => _service = service;

        [HttpGet, AllowAnonymous]
        public IActionResult Get()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult GetId(string id)
        {
            return Ok(_service.GetById(id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmbarcacaoViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut]
        public IActionResult Put([FromBody] EmbarcacaoViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                if (_service.GetById(entidade.Codigo.ToString()) == null)
                    return NotFound();

                return Ok(_service.Put(entidade));
            }

            return BadRequest("Classe inválida");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (_service.GetById(id) == null)
                return NotFound();

            return Ok(_service.Delete(id));
        }
    }
}
