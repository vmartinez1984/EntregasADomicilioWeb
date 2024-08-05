using System.ComponentModel.DataAnnotations;

namespace EntregaADomicilio.Web.Dtos
{
    public class PlatilloDto
    {
        public string Id { get; set; }

        public string EncodedKey { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string Categoria { get; set; }   
    }

    public class PlatilloDtoIn
    {        
        public IFormFile FormFile { get;  set; }
        
        public string EncodedKey { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }

        [Required]
        public string Categoria { get; set; }      
    }
}