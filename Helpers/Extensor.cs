using AutoMapper;
using EntregaADomicilio.Web.BusinessLayer;
using EntregaADomicilio.Web.Interfaces;
using EntregaADomicilio.Web.Repositories;
using EntregaADomicilio.Web.Stores;
using EntregasADomicilioWeb.BusinessLayer;
using EntregasADomicilioWeb.Repositories;

namespace EntregaADomicilio.Web.Helpers
{
    public static class Extensor
    {
        public static void AddBusinessLayer(this IServiceCollection services)
        {
            services.AddScoped<OpinionBl>();
            services.AddScoped<CategoriaBl>();
            services.AddScoped<PlatilloBl>();
            services.AddScoped<ArchivoBl>();
            services.AddScoped<InformacionBl>();
            services.AddScoped<UnitOfWork>();
        }


        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<OpinionRepository>();
            services.AddScoped<CategoriaRepository>();
            services.AddScoped<PlatilloRepository>();
            services.AddScoped<RestauranteRepository>();
            services.AddScoped<Repository>();
        }

        public static void AddMappers(this IServiceCollection services)
        {
            //Mappers
            var mapperConfig = new MapperConfiguration(mapperConfig =>
            {
                mapperConfig.AddProfile<Mappers>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddStores(this IServiceCollection services)
        {
            services.AddSingleton<IAlmacenadorDeArchivos, AlmacenDeArchivos>();
        }
    }
}