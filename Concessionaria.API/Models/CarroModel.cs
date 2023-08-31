using System;

namespace Concessionaria.API.Models
{
    public class CarroModel
    {
        public CarroModel(string nome, string marca, string categoria, int ano, double valor)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Marca = marca;
            Categoria = categoria;
            Ano = ano;
            Valor = valor;
        }

        public string Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public int Ano { get; set; }
        public double Valor { get; set; }
    }
}
