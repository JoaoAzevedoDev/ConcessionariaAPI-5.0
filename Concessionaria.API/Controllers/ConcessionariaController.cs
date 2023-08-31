using Concessionaria.API.Interfaces;
using Concessionaria.API.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concessionaria.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConcessionariaController : ControllerBase
    {

        private readonly ICarroServices _carroServices;
        public ConcessionariaController(ICarroServices carroServices)
        {
            _carroServices = carroServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarroModel>>> Get()
        {
            var token = HttpContext.GetTokenAsync("acess_token").Result;
            var carros = await _carroServices.GetAllCarros();
            return Ok(carros.Value);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarroModel>> Get(string id)
        {
            var carro = _carroServices.GetCarroById(id);
            if (carro == null)
                return BadRequest("Carro not found.");
            return Ok(carro.Result.Value);
        }
        [HttpPost]
        public async Task<ActionResult<List<CarroModel>>> AddCarro(CarroModel carro)
        {
            var request = await _carroServices.AddCarro(carro);
            return Ok(request.Value);
        }
        [HttpPut]
        public async Task<ActionResult<List<CarroModel>>> UpdateCarro(CarroModel request)
        {
            var dbCarro = await _carroServices.UpdateCarro(request);
            if (dbCarro == null)
                return BadRequest("Carro not found.");

            return Ok(dbCarro.Value);
        }

        [HttpDelete]
        public async Task<ActionResult<List<CarroModel>>> DeleteCarro(string id)
        {
            var request = await _carroServices.DeleteCarro(id);
            if (request == null)
                return BadRequest("Carro not found.");
            return Ok(request.Value);
        }
    }
}
