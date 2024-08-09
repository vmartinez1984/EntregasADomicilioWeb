using AutoMapper;
using EntregasADomicilioWeb.Dtos;
using EntregasADomicilioWeb.Entities;
using EntregasADomicilioWeb.Repositories;

namespace EntregasADomicilioWeb.BusinessLayer
{
    public class OpinionBl
    {
        private readonly OpinionRepository _repository;
        private readonly IMapper _mapper;

        public OpinionBl(OpinionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> AgregarAsync(OpinionDtoIn dto, string restaurante)
        {
            Opinion entidad;
            string id;

            entidad = _mapper.Map<Opinion>(dto);
            id = await _repository.AgregarAsync(restaurante, entidad);

            return id;
        }

          public async Task<List<OpinionDto>> ObtenerTodosAsync(string restaurante, bool? estaActivo = true)
        {
            List<Opinion> entities;
            List<OpinionDto> dtos;

            entities = await _repository.ObtenerTodosAsync(restaurante, estaActivo);
            dtos = _mapper.Map<List<OpinionDto>>(entities);

            return dtos;
        }
    }
}