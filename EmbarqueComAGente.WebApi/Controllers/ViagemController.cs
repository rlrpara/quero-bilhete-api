using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueroBilhete.Service.Interfaces;
using QueroBilhete.Service.ViewModels;
using QueroBilhete.Service.ViewModels.Consulta;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueroBilhete.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ViagemController : ControllerBase
    {
        private readonly IViagemService _service;

        public ViagemController(IViagemService service) => _service = service;

        [HttpGet, AllowAnonymous]
        public IActionResult Get() => Ok(_service.GetAll());

        [HttpGet("{id}"), AllowAnonymous]
        public IActionResult GetId(string id) => Ok(_service.GetById(id));

        [HttpPost]
        public IActionResult Post([FromBody] ViagemViewModel entidade)
        {
            if (ModelState.IsValid)
                return Created($"api/{RouteData.Values.First().Value}", _service.Post(entidade));

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPost("ConsultadeViagem")]
        public IActionResult PostConsultadeViagem([FromBody] ConsultaViagemOrigemDestino consulta)
        {
            if (ModelState.IsValid)
            {
                var retorno = new
                {
                    Paginacao = new
                    {
                        PaginaAtual = consulta.PaginaAtual,
                        QuantidadePorPagina = consulta.QuantidadePorPagina,
                        TotalRegistros = _service.PostConsultaViagem(consulta).Count()
                    },
                    Data = _service.PostConsultaViagem(consulta)
                };

                return Ok(retorno);
            }

            return BadRequest($"Classe inválida: {ModelState}");
        }

        [HttpPut]
        public IActionResult Put([FromBody] ViagemViewModel entidade)
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
