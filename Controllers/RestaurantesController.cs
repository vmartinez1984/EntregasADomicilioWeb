using EntregaADomicilio.Web.BusinessLayer;
using EntregasADomicilioWeb.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EntregasADomicilioWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public RestaurantesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarInfoAsync([FromForm] InfoDtoIn info)
        {
            string id;

            id = await _unitOfWork.Informacion.AgregarInfoAsync(info);

            return Ok(new { id });
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerAsync(string id)
        {
            return Ok();
        }

        [HttpGet("{restaurante}/imagenes/{imagenId}")]
        public async Task<IActionResult> ObtenerImagenAsync(string restaurante, string imagenId)
        {
            byte[] bytes;

            bytes = await _unitOfWork.Informacion.ObtenerBytesAsync(restaurante, imagenId);

            return File(bytes, "image/png");
        }
    }
}