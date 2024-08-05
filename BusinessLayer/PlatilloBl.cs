using AutoMapper;
using EntregaADomicilio.Web.Dtos;
using EntregaADomicilio.Web.Entities;
using EntregaADomicilio.Web.Interfaces;
using EntregaADomicilio.Web.Repositories;

namespace EntregaADomicilio.Web.BusinessLayer
{
    public class PlatilloBl //: IPlatilloBl
    {
        private readonly Repository _repository;
        private readonly IAlmacenadorDeArchivos _almacenadorDeArchivosFirebaseStorage;
        private readonly IMapper _mapper;
        private readonly string _contenedor = "platillos";

        public PlatilloBl(
            Repository repositorySql,
            IAlmacenadorDeArchivos almacenadorDeArchivosFirebaseStorage,
            IMapper mapper
        )
        {
            _repository = repositorySql;
            _almacenadorDeArchivosFirebaseStorage = almacenadorDeArchivosFirebaseStorage;
            _mapper = mapper;
        }

        //public async Task ActualizarAsync(int id, PlatilloDtoIn platillo)
        //{
        //    Platillo entity;

        //    entity = await _repositorySql.Platillo.ObtenerPorIdAsync(id);
        //    entity = _mapper.Map(platillo, entity);
        //    if (platillo.FormFile != null)
        //    {
        //        foreach (var item in entity.ListaDeArchivos)
        //        {
        //            item.EstaActivo = false;
        //        }
        //        entity.ListaDeArchivos.AddRange(await ObtenerListaDeArchivosAsync(platillo));
        //    }

        //    await _repositorySql.Platillo.ActualizarAsync(entity);
        //}

        public async Task<string> AgregarAsync(string restaurante, PlatilloDtoIn platillo)
        {
            Platillo entity;

            if (string.IsNullOrEmpty(platillo.EncodedKey))
                platillo.EncodedKey = Guid.NewGuid().ToString();
            entity = _mapper.Map<Platillo>(platillo);
            entity.Archivo = await ObtenerArchivoAsync(platillo);
            await _repository.Platillo.AgregarAsync(restaurante, entity);

            return entity.Id;
        }

        private async Task<Archivo> ObtenerArchivoAsync(PlatilloDtoIn platillo)
        {
            Archivo archivo;
            string aliasDelArchivo;

            aliasDelArchivo = $"{platillo.EncodedKey}{Path.GetExtension(platillo.FormFile.FileName)}";
            archivo = await GuardarEnAlamcenVMtz(platillo, aliasDelArchivo);

            return archivo;
        }

        private async Task<Archivo> GuardarEnAlamcenVMtz(PlatilloDtoIn platillo, string aliasDelArchivo)
        {
            try
            {
                string rutaDelArchvo;

                rutaDelArchvo = await _almacenadorDeArchivosFirebaseStorage.Guardar(_contenedor, aliasDelArchivo, platillo.FormFile);

                return new Archivo
                {
                    NombreDelArchivo = platillo.FormFile.FileName,
                    NombreDelAlmacen = "FireStore",
                    RutaDelArchivo = rutaDelArchvo,
                    ContentType = platillo.FormFile.ContentType,
                    EstaActivo = true,
                    FechaDeRegistro = DateTime.Now,
                    AliasDelArchivo = aliasDelArchivo
                };
            }
            catch (Exception)
            {

                return null;
            }
        }

        //public async Task BorrarAsync(int id)
        //{
        //    Platillo platillo;

        //    platillo = await _repositorySql.Platillo.ObtenerPorIdAsync(id);
        //    platillo.EstaActivo = false;

        //    await _repositorySql.Platillo.ActualizarAsync(platillo);
        //}

        //public async Task<byte[]> ObtenerBytesAsync(int platilloId)
        //{
        //    byte[] bytes;
        //    Platillo platillo;

        //    platillo = await _repositorySql.Platillo.ObtenerPorIdAsync(platilloId);
        //    try
        //    {
        //        bytes = await ObtenerBytesDeAlmacenAsync(platillo.ListaDeArchivos, AlmacenDeArchivos.AlmacenVMtz);
        //    }
        //    catch (Exception)
        //    {
        //        bytes = await ObtenerBytesDeAlmacenAsync(platillo.ListaDeArchivos, AlmacenDeArchivos.FirebaseStorage);
        //    }

        //    return bytes;
        //}

        private async Task<byte[]> ObtenerBytesDeAlmacenAsync(Archivo archivo)
        {
            byte[] bytes = null;

            bytes = await _almacenadorDeArchivosFirebaseStorage.Obtener(archivo.RutaDelArchivo);

            return bytes;
        }

        public async Task<List<PlatilloDto>> ObtenerTodosAsync(string restaurante, bool? estaActivo = true)
        {
            List<Platillo> entities;
            List<PlatilloDto> dtos;

            entities = await _repository.Platillo.ObtenerTodosAsync(restaurante, estaActivo);
            dtos = _mapper.Map<List<PlatilloDto>>(entities);

            return dtos;
        }

        public async Task<PlatilloDto> ObtenerPorIdAsync(string restaurante, string platilloId)
        {
            Platillo platillo;
            PlatilloDto platilloDto;

            platillo = await _repository.Platillo.ObtenerPorIdAsync(restaurante, platilloId);
            platilloDto = _mapper.Map<PlatilloDto>(platillo);

            return platilloDto;
        }

        public async Task<byte[]> ObtenerBytesAsync(string restaurante, string platilloId)
        {
            Platillo platillo;

            platillo = await _repository.Platillo.ObtenerPorIdAsync(restaurante, platilloId);

            return await ObtenerBytesDeAlmacenAsync(platillo.Archivo);
        }

        public async Task ActualizarAsync(string restaurante, string id, PlatilloDtoIn platillo)
        {
            Platillo platilloEntity;

            platilloEntity = await _repository.Platillo.ObtenerPorIdAsync(restaurante, id);
            //platilloEntity = _mapper.Map(platillo, platilloEntity);
            platilloEntity.Descripcion = platillo.Descripcion;
            platilloEntity.Categoria = platillo.Categoria;
            platilloEntity.Precio =platillo.Precio;
            platilloEntity.EncodedKey = string.IsNullOrEmpty(platillo.EncodedKey)? platilloEntity.EncodedKey : platillo.EncodedKey;
            if (platillo.FormFile is not null)
            {
                Archivo archivo;

                archivo = platilloEntity.Archivo;
                archivo.RutaDelArchivo =
                await _almacenadorDeArchivosFirebaseStorage.EditarArchivo(
                    _contenedor,
                    archivo.AliasDelArchivo,
                    platillo.FormFile
                );
            }

            await _repository.Platillo.ActualizarAsync(restaurante, platilloEntity);
        }

        public Task BorrarAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}