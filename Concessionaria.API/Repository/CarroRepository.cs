using Concessionaria.API.Configuration;
using Concessionaria.API.Interfaces;
using Concessionaria.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Concessionaria.API.Repository
{
    public class CarroRepository : ICarroRepository
    {
        private readonly IMongoCollection<CarroModel> _carroCollection;
        private readonly IMongoCollection<UserCredential> _userCollection;
        public CarroRepository(IDatabaseConfig databaseConfig)
        {
            var client = new MongoClient(databaseConfig.ConnectionString);
            var database = client.GetDatabase(databaseConfig.DatabaseName);
            _carroCollection = database.GetCollection<CarroModel>("carros");
            _userCollection = database.GetCollection<UserCredential>("users");
        }

        public async Task<ActionResult<List<CarroModel>>> AddCarro(CarroModel carro)
        {
            await _carroCollection.InsertOneAsync(carro);
            return await _carroCollection.Find(h => true).ToListAsync();
        }

        public async Task<ActionResult<List<CarroModel>>> DeleteCarro(string id)
        {
            var carro = _carroCollection.Find(h => h.Id == id).FirstOrDefault();
            if (carro == null)
                return null;
            _carroCollection.DeleteOne(h => h.Id == id);
            return await _carroCollection.Find(h => true).ToListAsync();
        }

        public async Task<ActionResult<List<CarroModel>>> GetAllCarros()
        {
            var carros = await _carroCollection.Find(h => true).ToListAsync();
            return carros;
        }

        public async Task<ActionResult<CarroModel>> GetCarroById(string id)
        {
            var carro = _carroCollection.Find(h => h.Id == id).FirstOrDefault();
            if (carro == null)
                return null;
            return carro;
        }

        public async Task<ActionResult<List<CarroModel>>> UpdateCarro(CarroModel request)
        {
            var dbCarro = _carroCollection.Find(h => h.Id == request.Id).FirstOrDefault();
            if (dbCarro == null)
                return null;

            dbCarro.Nome = request.Nome;
            dbCarro.Marca = request.Marca;
            dbCarro.Categoria = request.Categoria;
            dbCarro.Ano = request.Ano;
            dbCarro.Valor = request.Valor;

            await _carroCollection.ReplaceOneAsync(h => h.Id == dbCarro.Id, dbCarro);
            return await _carroCollection.Find(h => true).ToListAsync();
        }
    }
}
