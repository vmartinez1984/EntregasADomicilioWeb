using EntregaADomicilio.Web.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EntregaADomicilio.Web.Repositories
{
    public class PlatilloRepository
    {
        private IMongoCollection<Platillo> _collection { get; set; }
        private readonly MongoClient _mongoClient;

        public PlatilloRepository(IConfiguration configurations)
        {
            _mongoClient = new MongoClient(configurations.GetConnectionString("MongoDb"));
        }

        private void ConfigurarDb(string restaurante)
        {
            var mongoDatabase = _mongoClient.GetDatabase(restaurante);
            _collection = mongoDatabase.GetCollection<Platillo>("Platillos");
        }

        internal async Task ActualizarAsync(string restaurante, Platillo platilloEntity)
        {
            ConfigurarDb(restaurante);
            await _collection.ReplaceOneAsync(x => x._id == platilloEntity._id, platilloEntity);
        }

        public async Task AgregarAsync(string restaurante, Platillo entity)
        {
            ConfigurarDb(restaurante);
            entity.Id = await ObtenerIdAscyn();
            await _collection.InsertOneAsync(entity);
        }

        private async Task<string> ObtenerIdAscyn()
        {
            var id = await _collection.CountDocumentsAsync(_ => true);
            id = id + 1;

            return id.ToString();
        }

        internal async Task<Platillo> ObtenerPorIdAsync(string restaurante, string id)
        {
            ConfigurarDb(restaurante);
            Platillo item;

            item = (await _collection.FindAsync(
               new BsonDocument("$or", new BsonArray
               {
                    new BsonDocument("Id", id),
                    new BsonDocument("EncodedKey", id)
               })
           )).FirstOrDefault();

            return item;
        }

        internal async Task<List<Platillo>> ObtenerTodosAsync(string restaurante, bool? estaActivo = true)
        {
            ConfigurarDb(restaurante);
            List<Platillo> platillos;

            platillos = (await _collection.FindAsync(new BsonDocument(nameof(Platillo.EstaActivo), estaActivo))).ToList();

            return platillos;
        }
    }
}