using EntregaADomicilio.Web.BusinessLayer;
using EntregasADomicilioWeb.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EntregasADomicilioWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpinionesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public OpinionesController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Lista de categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet("Restaurantes/{restaurante}")]
        public async Task<IEnumerable<OpinionDto>> Get(string restaurante)
        {
            List<OpinionDto> lista;

            lista = await _unitOfWork.Opinion.ObtenerTodosAsync(restaurante);

            return lista;
        }

        // POST api/<CategoriasController>
        [HttpPost("Restaurantes/{restaurante}")]
        public async Task<IActionResult> Post([FromBody] OpinionDtoIn categoria, string restaurante)
        {
            string id;
            
            id = await _unitOfWork.Opinion.AgregarAsync(categoria, restaurante);

            return Created($"/Opiniones/{id}", new { Id = id });
        }

        // /// <summary>
        // /// ACtualizar categoria
        // /// </summary>
        // /// <param name="id"></param>
        // /// <param name="categoria"></param>
        // /// <returns></returns>
        // [HttpPut("{id}/Restaurantes/{restaurante}")]
        // public async Task<IActionResult> Put(string restaurante, string id, [FromBody] CategoriaDtoIn categoria)
        // {
        //     await _unitOfWork.Categoria.ActualizarAsync(restaurante, id, categoria);

        //     return Accepted();
        // }
    }
}