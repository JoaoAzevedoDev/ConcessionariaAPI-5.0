namespace Concessionaria.API.Configuration
{
    public interface IDatabaseConfig
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }

    }
}
