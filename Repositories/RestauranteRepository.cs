using EntregasADomicilioWeb.Entities;
using MongoDB.Driver;

namespace EntregasADomicilioWeb.Repositories
{
    public class RestauranteRepository
    {
        private IMongoCollection<Informacion> _collection { get; set; }
        private readonly MongoClient _mongoClient;

        public RestauranteRepository(IConfiguration configurations)
        {
            _mongoClient = new MongoClient(configurations.GetConnectionString("MongoDb"));
        }

        private void ConfigurarDb(string restaurante)
        {
            var mongoDatabase = _mongoClient.GetDatabase(restaurante);
            _collection = mongoDatabase.GetCollection<Informacion>("Informacion");
        }

        internal async Task<string> AgregarAsync(Informacion informacion)
        {
            ConfigurarDb(informacion.EncodedKey);
            
            informacion.Id = await ObtenerIdAscyn();
            await _collection.InsertOneAsync(informacion);

            return informacion.Id;
        }

         private async Task<string> ObtenerIdAscyn()
        {
            var id = await _collection.CountDocumentsAsync(_ => true);
            id = id + 1;

            return id.ToString();
        }
    }
}