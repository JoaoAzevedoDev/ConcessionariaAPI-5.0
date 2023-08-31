using Concessionaria.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concessionaria.API.Interfaces
{
    public interface ICarroRepository
    {
        Task<ActionResult<List<CarroModel>>> GetAllCarros();
        Task<ActionResult<CarroModel>> GetCarroById(string id);
        Task<ActionResult<List<CarroModel>>> AddCarro(CarroModel carro);
        Task<ActionResult<List<CarroModel>>> UpdateCarro(CarroModel request);
        Task<ActionResult<List<CarroModel>>> DeleteCarro(string id);

    }
}
