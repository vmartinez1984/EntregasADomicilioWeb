using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EntregaADomicilio.Web.Dtos
{
    public class CategoriaDto
    {
        public string Id { get; set; }

        public string Nombre { get; set; }        
    }

    public class CategoriaDtoIn
    {
        [Required, MinLength(5), MaxLength(50)]
        [DefaultValue("")]
        public string Nombre { get; set; }
                
        public string EncodedKey { get; set; }
    }
}