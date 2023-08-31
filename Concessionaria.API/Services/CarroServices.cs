using Concessionaria.API.Interfaces;
using Concessionaria.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concessionaria.API.Services
{
    public class CarroServices : ICarroServices
    {
        private readonly ICarroRepository _carroRepository;
        public CarroServices(ICarroRepository carroRepository)
        {
            _carroRepository = carroRepository;
        }
        public Task<ActionResult<List<CarroModel>>> AddCarro(CarroModel carro)
        {
            return _carroRepository.AddCarro(carro);
        }

        public Task<ActionResult<List<CarroModel>>> DeleteCarro(string id)
        {
            return _carroRepository.DeleteCarro(id);
        }

        public Task<ActionResult<List<CarroModel>>> GetAllCarros()
        {
            var carros = _carroRepository.GetAllCarros();
            return carros;
        }

        public Task<ActionResult<CarroModel>> GetCarroById(string id)
        {
            return _carroRepository.GetCarroById(id);
        }

        public Task<ActionResult<List<CarroModel>>> UpdateCarro(CarroModel request)
        {
            return _carroRepository.UpdateCarro(request);
        }
    }
}
