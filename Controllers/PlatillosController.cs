using EntregaADomicilio.Web.BusinessLayer;
using EntregaADomicilio.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EntregaADomicilio.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatillosController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PlatillosController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene los platillos
        /// </summary>
        /// <returns></returns>
        [HttpGet("Restaurantes/{restaurante}")]
        public async Task<IActionResult> ObtenerTodos(string restaurante)
        {
            List<PlatilloDto> platillos;

            platillos = await _unitOfWork.Platillo.ObtenerTodosAsync(restaurante);

            return Ok(platillos);
        }

        /// <summary>
        /// Obtiene la imagen del platillo por id
        /// </summary>
        /// <param name="platilloId"></param>        
        [HttpGet("{platilloId}/Imagen/Restaurantes/{restaurante}")]
        public async Task<IActionResult> ObtenerImagenPorPlatilloId(string restaurante, string platilloId)
        {
            byte[] bytes;

            bytes = await _unitOfWork.Platillo.ObtenerBytesAsync(restaurante, platilloId);

            return File(bytes, "image/png");
            //return File(memoryStream.ToArray(), "image/png", fileDownloadName: data.ObjectName); 
        }

        /// <summary>
        /// Obtine el platillo por platilloId
        /// </summary>
        /// <param name="platilloId"></param>
        /// <response code="200">PlatilloDto</response>
        [HttpGet("{platilloId}/Restaurantes/{restaurante}")]
        public async Task<IActionResult> ObtenerPorPlatilloId(string restaurante, string platilloId)
        {
            PlatilloDto platillo;

            platillo = await _unitOfWork.Platillo.ObtenerPorIdAsync(restaurante, platilloId);

            return Ok(platillo);
        }

        /// <summary>
        /// Agregar un platillo al menu
        /// </summary>
        /// <param name="platillo"></param>
        /// <returns></returns>
        [HttpPost("Restaurantes/{restaurante}")]
        public async Task<IActionResult> AgregarAsync(string restaurante,[FromForm] PlatilloDtoIn platillo)
        {
            string id;
            bool existe;
            PlatilloDto platilloDto;

            existe = await _unitOfWork.Categoria.ExisteAsync(restaurante, platillo.Categoria);
            if (!existe)
            {
                this.ModelState.AddModelError(nameof(PlatilloDtoIn.Categoria), "No existe la categoria");

                return BadRequest();
            }
            if (!string.IsNullOrEmpty(platillo.EncodedKey))
            {
                platilloDto = await _unitOfWork.Platillo.ObtenerPorIdAsync(restaurante, platillo.EncodedKey);
                if (platilloDto is not null)
                    return Ok(platilloDto);
            }
            id = await _unitOfWork.Platillo.AgregarAsync(restaurante, platillo);

            return Created($"Platillos/{id}", new { Id = id });
        }

        /// <summary>
        /// No implementado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="platillo"></param>
        [HttpPut("{id}/Restaurantes/{restaurante}")]
        public async Task<IActionResult> Put(string restaurante,string id, [FromForm] PlatilloDtoIn platillo)
        {
            await _unitOfWork.Platillo.ActualizarAsync( restaurante,id, platillo);

            return Accepted();
        }
    }
}