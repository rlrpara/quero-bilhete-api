using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace QueroBilhete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly IEmpresaService _service;

        public EmpresaController(IEmpresaService service)
            => _service = service;

        [HttpGet, Authorize]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}"), Authorize]
        public IActionResult GetId(string id) => Ok(_service.GetById(id));

        [HttpPost, Authorize]
        public IActionResult Post([FromBody] EmpresaViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut, Authorize]
        public IActionResult Put([FromBody] EmpresaViewModel entidade)
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
