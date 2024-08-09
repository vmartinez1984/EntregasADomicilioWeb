using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EntregasADomicilioWeb.Entities
{
    public class Opinion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Nombre { get; set; }

        public string Telefono { get; set; }

        public string Correo { get; set; }

        public string Descripcion { get; set; }
        public string Id { get; set; }

        public bool EstaActivo { get; set; } = true;

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}