using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using System.Linq;

namespace QueroBilhete.WebApi.Controllers
{
    [Route("webapi/[controller]")]
    [ApiController, Authorize]
    public class PassageiroController : ControllerBase
    {
        private readonly IPassageiroService _service;

        public PassageiroController(IPassageiroService service)
            => _service = service;

        [HttpGet]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}")]
        public IActionResult GetId(string id) => Ok(_service.GetById(id));

        [HttpPost]
        public IActionResult Post([FromBody] PassageiroViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut]
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

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (_service.GetById(id) == null)
                return NotFound();

            return Ok(_service.Delete(id));
        }
    }
}
