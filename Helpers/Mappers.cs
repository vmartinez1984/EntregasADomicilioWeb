using AutoMapper;
using EntregaADomicilio.Web.Dtos;
using EntregaADomicilio.Web.Entities;

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
        }
    }
}