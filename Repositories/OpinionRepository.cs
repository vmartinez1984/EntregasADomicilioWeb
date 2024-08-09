using EntregasADomicilioWeb.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EntregasADomicilioWeb.Repositories
{
    public class OpinionRepository
    {
        private IMongoCollection<Opinion> _collection { get; set; }
        private readonly MongoClient _mongoClient;

        public OpinionRepository(IConfiguration configurations)
        {
            _mongoClient = new MongoClient(configurations.GetConnectionString("MongoDb"));
        }

        private void ConfigurarDb(string restaurante)
        {
            var mongoDatabase = _mongoClient.GetDatabase(restaurante);
            _collection = mongoDatabase.GetCollection<Opinion>("Opiniones");
        }

        internal async Task ActualizarAsync(string restaurante, Opinion entity)
        {
            ConfigurarDb(restaurante);
            await _collection.ReplaceOneAsync(x => x._id == entity._id, entity);
        }

        public async Task<string> AgregarAsync(string restaurante, Opinion entity)
        {
            ConfigurarDb(restaurante);
            entity.Id = await ObtenerIdAscyn();
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        private async Task<string> ObtenerIdAscyn()
        {
            var id = await _collection.CountDocumentsAsync(_ => true);
            id = id + 1;

            return id.ToString();
        }

        internal async Task<Opinion> ObtenerPorIdAsync(string restaurante, string id)
        {
            ConfigurarDb(restaurante);
            Opinion item;

            item = (await _collection.FindAsync(
               new BsonDocument("$or", new BsonArray
               {
                    new BsonDocument("Id", id),
                    new BsonDocument("EncodedKey", id)
               })
           )).FirstOrDefault();

            return item;
        }

        internal async Task<List<Opinion>> ObtenerTodosAsync(string restaurante, bool? estaActivo = true)
        {
            ConfigurarDb(restaurante);
            List<Opinion> platillos;

            platillos = (await _collection.FindAsync(new BsonDocument(nameof(Opinion.EstaActivo), estaActivo))).ToList();

            return platillos;
        }

        // internal async Task ActualizarAsync(string restaurante, Platillo platilloEntity)
        // {
        //     ConfigurarDb(restaurante);
        //     await _collection.ReplaceOneAsync(x => x._id == platilloEntity._id, platilloEntity);
        // }

        // private async Task<string> ObtenerIdAscyn()
        // {
        //     var id = await _collection.CountDocumentsAsync(_ => true);
        //     id = id + 1;

        //     return id.ToString();
        // }

        // internal async Task<Platillo> ObtenerPorIdAsync(string restaurante, string id)
        // {
        //     ConfigurarDb(restaurante);
        //     Platillo item;

        //     item = (await _collection.FindAsync(
        //        new BsonDocument("$or", new BsonArray
        //        {
        //             new BsonDocument("Id", id),
        //             new BsonDocument("EncodedKey", id)
        //        })
        //    )).FirstOrDefault();

        //     return item;
        // }

        // internal async Task<List<Platillo>> ObtenerTodosAsync(string restaurante, bool? estaActivo = true)
        // {
        //     ConfigurarDb(restaurante);
        //     List<Platillo> platillos;

        //     platillos = (await _collection.FindAsync(new BsonDocument(nameof(Platillo.EstaActivo), estaActivo))).ToList();

        //     return platillos;
        // }
    }
}