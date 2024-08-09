using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EntregaADomicilio.Web.Entities
{
    public class Archivo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Id { get; set; }

        public string EncodedKey { get; set; }
        
        public string NombreDelArchivo { get; set; }
        
        public string AliasDelArchivo { get; set; }
        
        public string RutaDelArchivo { get; set; }
        
        public string ContentType { get; set; }

        public string NombreDelAlmacen { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public string Nota { get; set; }

        public bool EstaActivo { get; set; }
    }
}