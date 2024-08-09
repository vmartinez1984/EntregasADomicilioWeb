using AutoMapper;
using EntregaADomicilio.Web.Entities;
using EntregasADomicilioWeb.Dtos;
using EntregasADomicilioWeb.Entities;
using EntregasADomicilioWeb.Repositories;

namespace EntregasADomicilioWeb.BusinessLayer
{
    public class InformacionBl
    {
        private readonly RestauranteRepository _repository;
        private readonly IMapper _mapper;
        private readonly ArchivoBl _archivoBl;
        private readonly string _contenedor;

        public InformacionBl(RestauranteRepository repository, IMapper mapper, ArchivoBl archivoBl)
        {
            _repository = repository;
            _mapper = mapper;
            _archivoBl = archivoBl;
            _contenedor = "platillos";
        }

        public async Task<string> AgregarInfoAsync(InfoDtoIn info)
        {
            Informacion informacion;
            List<Archivo> archivos = new List<Archivo>();
            string aliasDelArchivo;
            string id;

            foreach (var item in info.Imagenes)
            {
                string guid;
                Archivo archivo;

                guid = Guid.NewGuid().ToString();
                aliasDelArchivo = $"{guid}{Path.GetExtension(item.FormFile.FileName)}";
                archivo = await _archivoBl.AgregarAsync(item.FormFile, aliasDelArchivo, _contenedor);
                archivo.Nota = "Carrusel";
                archivo.EncodedKey = guid;

                archivos.Add(archivo);
            }

            informacion = _mapper.Map<Informacion>(info);
            informacion.Imagenes = archivos;
            informacion.Otros = info.Otros;
            id = await _repository.AgregarAsync(informacion);

            return id;
        }

        internal async Task<byte[]> ObtenerBytesAsync(string restaurante, string imagenId)
        {
            throw new NotImplementedException();
        }
    }
}