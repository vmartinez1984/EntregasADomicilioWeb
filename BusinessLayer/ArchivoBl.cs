using EntregaADomicilio.Web.Entities;
using EntregaADomicilio.Web.Interfaces;

namespace EntregasADomicilioWeb.BusinessLayer
{
    public class ArchivoBl
    {
         private readonly IAlmacenadorDeArchivos _almacenadorDeArchivos;
        public ArchivoBl(IAlmacenadorDeArchivos almacenDeArchivos)
        {
            _almacenadorDeArchivos = almacenDeArchivos;
        }

        public async Task<Archivo> AgregarAsync(IFormFile formFile, string aliasDelArchivo, string contenedor)
        {
            try
            {
                string rutaDelArchvo;

                rutaDelArchvo = await _almacenadorDeArchivos.Guardar(contenedor, aliasDelArchivo, formFile);

                return new Archivo
                {
                    NombreDelArchivo = formFile.FileName,
                    NombreDelAlmacen = "FireStore",
                    RutaDelArchivo = rutaDelArchvo,
                    ContentType = formFile.ContentType,
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

        internal async Task<string> EditarAsync(string contenedor, string aliasDelArchivo, IFormFile formFile)
        {
            return await _almacenadorDeArchivos.EditarArchivo(contenedor, aliasDelArchivo, formFile);
        }

        internal async Task<byte[]> ObtenerBytesAsync(string rutaDelArchivo)
        {
            byte[] bytes = null;

            bytes = await _almacenadorDeArchivos.Obtener(rutaDelArchivo);

            return bytes;
        }
    }
}