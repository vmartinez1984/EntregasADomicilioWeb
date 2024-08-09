using EntregaADomicilio.Web.Entities;

namespace EntregasADomicilioWeb.Entities
{
    public class Informacion
    {
        public string EncodedKey { get; set; }
        public string Nombre { get; set; }

        public string Presentacion { get; set; }

        public Direccion Direccion { get; set; }

        public Dictionary<string, string> Otros { get; set; }

        public List<Archivo> Imagenes { get; set; }
        public string Id { get; set; }
    }

    public class Direccion
    {
        public string CalleYNumero { get; set; }

        public string Colonia { get; set; }

        public string CodigoPostal { get; set; }

        public string Municipio { get; set; }

        public string Estado { get; set; }

        public string Referencias { get; set; }

        public string CoordenadasGps { get; set; }
    }
}
