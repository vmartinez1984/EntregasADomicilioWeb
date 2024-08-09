using System.ComponentModel.DataAnnotations;

namespace EntregasADomicilioWeb.Dtos
{
    public class InfoDtoIn
    {
        [Required]
        public string EncodedKey { get; set; }

        [Required]
        public string Nombre { get; set; }

        public DireccionDtoIn Direccion { get; set; }

        public List<ImagenDtoIn> Imagenes { get; set; }

        public Dictionary<string, string> Otros { get; set; }
    }

    public class ImagenDtoIn
    {
        public IFormFile FormFile { get; set; }

        public string Etiqueta { get; set; }
    }

    public class DireccionDtoIn
    {
        public string CalleYNumero { get; set; }

        public string Colonia { get; set; }

        public string CodigoPostal { get; set; }

        public string Municipio { get; set; }

        public string Estado { get; set; }

        public string Referencias { get; set; }

        public string CoordenadasGps { get; set; }
    }

    public class InfoDto
    {
        [Required]
        public string EncodedKey { get; set; }

        [Required]
        public string Nombre { get; set; }

        public DireccionDto Direccion { get; set; }

        public List<ImagenDto> Imagenes { get; set; }

        public Dictionary<string, string> Otros { get; set; }
    }

    public class ImagenDto
    {
        public string Etiqueta { get; set; }

        public string Id { get; set; }

        public string Ruta { get; set; }
    }

    public class DireccionDto
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