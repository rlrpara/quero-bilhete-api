using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace QueroBilhete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class PassageiroController : ControllerBase
    {
        private readonly IPassageiroService _service;

        public PassageiroController(IPassageiroService service)
            => _service = service;

        [HttpGet, Authorize]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}"), Authorize]
        public IActionResult GetId(string id) => Ok(_service.GetById(id));

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] PassageiroViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut, Authorize]
        public IActionResult Put([FromBody] PassageiroViewModel entidade)
        {
            if (ModelState.IsValid)
            {
                if (_service.GetById(entidade.Codigo.ToString()) == null)
                    return NotFound();

                return Ok(_service.Put(entidade));
            }

            return BadRequest("Classe inválida");
        }

        [HttpDelete("{id}"), Authorize]
        public IActionResult Delete(string id)
        {
            if (_service.GetById(id) == null)
                return NotFound();

            return Ok(_service.Delete(id));
        }
    }
}
