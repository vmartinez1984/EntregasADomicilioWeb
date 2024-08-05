using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EntregaADomicilio.Web.Entities
{
    public class Categoria
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Id { get; set; }

        public string EncodedKey { get; set; }

        public string Nombre { get; set; }

        public bool EstaActivo { get; set; } = true;
    }
}