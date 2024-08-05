using AutoMapper;
using EntregaADomicilio.Web.Dtos;
using EntregaADomicilio.Web.Entities;
using EntregaADomicilio.Web.Repositories;

namespace EntregaADomicilio.Web.BusinessLayer
{
    public class CategoriaBl
    {
        private readonly Repository _repository;
        private readonly IMapper _mapper;

        public CategoriaBl(Repository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        internal async Task ActualizarAsync(string restaurante, string id, CategoriaDtoIn categoria)
        {
            Categoria entidad;

            entidad = await _repository.Categoria.ObtenerPorIdAsync(restaurante, id);
            entidad.Nombre = categoria.Nombre;

            await _repository.Categoria.ActualizarAsync(restaurante,entidad);
        }

        public async Task<string> AgregarAsync(CategoriaDtoIn categoria, string restaurante)
        {
            Categoria entity;

            entity = _mapper.Map<Categoria>(categoria);
            if (string.IsNullOrEmpty(categoria.EncodedKey))
                entity.EncodedKey = Guid.NewGuid().ToString();
            await _repository.Categoria.AgregarAsync(entity, restaurante);

            return entity.Id;
        }

        internal async Task<bool> ExisteAsync(string restaurante, string categoria)
        {
           return await _repository.Categoria.ExisteAsync(restaurante, categoria);
        }

        internal async Task<CategoriaDto> ObtenerPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        internal async Task<List<CategoriaDto>> ObtenerTodos(string restaurante)
        {
            List<Categoria> entidades;
            List<CategoriaDto> dtos;

            entidades = await _repository.Categoria.ObtenerTodosAsync(restaurante);
            dtos = _mapper.Map<List<CategoriaDto>>(entidades);

            return dtos;
        }
    }
}