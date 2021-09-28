using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueroBilhete.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class TipoPassagemController : ControllerBase
    {
        private readonly ITipoPassagemService _service;

        public TipoPassagemController(ITipoPassagemService service)
            => _service = service;

        [HttpGet, Authorize]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetId(string id) => Ok(_service.GetById(id));

        [HttpPost]
        public IActionResult Post([FromBody] TipoPassagemViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut]
        public IActionResult Put([FromBody] TipoPassagemViewModel entidade)
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
