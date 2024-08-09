using AutoMapper;
using EntregaADomicilio.Web.Dtos;
using EntregaADomicilio.Web.Entities;
using EntregasADomicilioWeb.Dtos;
using EntregasADomicilioWeb.Entities;

namespace EntregaADomicilio.Web.Helpers
{
    public class Mappers: Profile
    {
        public Mappers()
        {
            CreateMap<PlatilloDtoIn, Platillo>();
            CreateMap<Platillo, PlatilloDto>();
            CreateMap<CategoriaDtoIn,Categoria>();      
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<OpinionDtoIn, Opinion>(); 
            CreateMap<Opinion, OpinionDto>();   
            CreateMap<DireccionDtoIn,Direccion>();
            CreateMap<InfoDtoIn, Informacion>();
        }
    }
}