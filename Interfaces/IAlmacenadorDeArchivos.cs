
namespace EntregaADomicilio.Web.Interfaces
{
    public interface IAlmacenadorDeArchivos
    {
        Task<string> EditarArchivo(string contenedor, string aliasDelArchivo, IFormFile formFile);
        Task<string> Guardar(string contenedor, string aliasDelArchivo, IFormFile formFile);
        Task<byte[]> Obtener(string rutaDelArchivo);
    }
}