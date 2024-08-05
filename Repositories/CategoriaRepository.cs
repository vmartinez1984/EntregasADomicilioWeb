using EntregaADomicilio.Web.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EntregaADomicilio.Web.Repositories
{
    public class CategoriaRepository
    {
        private IMongoCollection<Categoria> _collection { get; set; }
        private readonly MongoClient _mongoClient;

        public CategoriaRepository(IConfiguration configurations)
        {
            _mongoClient = new MongoClient(configurations.GetConnectionString("MongoDb"));
        }

        private void ConfigurarDb(string restaurante)
        {
            var mongoDatabase = _mongoClient.GetDatabase(restaurante);
            _collection = mongoDatabase.GetCollection<Categoria>("Categorias");
        }

        public async Task<List<Categoria>> ObtenerTodosAsync(string restaurante)
        {
            ConfigurarDb(restaurante);
            List<Categoria> lista;

            lista = (await _collection.FindAsync(_ => true)).ToList();

            return lista;
        }

        internal async Task<string> AgregarAsync(Categoria entida, string restaurante)
        {
            ConfigurarDb(restaurante);

            entida.Id = await ObtenerIdAscyn();
            await _collection.InsertOneAsync(entida);

            return entida.Id;
        }

        private async Task<string> ObtenerIdAscyn()
        {
            var id = await _collection.CountDocumentsAsync(_ => true);
            id = id + 1;

            return id.ToString();
        }

        public async Task<bool> ExisteAsync(string restaurante, string categoria)
        {
            ConfigurarDb(restaurante);
            long total;

            total = await _collection.CountDocumentsAsync(
                new BsonDocument("Nombre", categoria)
            );

            return total == 0 ? false : true;
        }

        internal async Task<Categoria> ObtenerPorIdAsync(string restaurante, string id)
        {
            ConfigurarDb(restaurante);
            Categoria item;

            item = (await _collection.FindAsync(
               new BsonDocument("$or", new BsonArray
               {
                    new BsonDocument("Id", id),
                    new BsonDocument("EncodedKey", id)
               })
           )).FirstOrDefault();

            return item;
        }

        internal async Task ActualizarAsync(string restaurante, Categoria entidad)
        {
            ConfigurarDb(restaurante);
            await _collection.ReplaceOneAsync(entidad._id, entidad);
        }
    }
}