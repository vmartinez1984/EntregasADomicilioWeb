using System.ComponentModel.DataAnnotations;

namespace EntregasADomicilioWeb.Dtos
{
    public class OpinionDtoIn
    {
        [Required]
        public string Nombre { get; set; }

        [MaxLength(10)]
        [MinLength(10)]
        public string Telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Correo { get; set; }

        [Required]
        [MaxLength(250)]
        public string Descripcion { get; set; }
    }

     public class OpinionDto
    {        
        public string Nombre { get; set; }
     
        public string Telefono { get; set; }

        public string Correo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaDeRegistro { get; set; }
    }
}