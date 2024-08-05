using EntregaADomicilio.Web.BusinessLayer;
using EntregaADomicilio.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EntregaADomicilio.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoriasController(UnitOfWork unitOfWorkBl)
        {
            _unitOfWork = unitOfWorkBl;
        }

        /// <summary>
        /// Lista de categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet("Restaurantes/{restaurante}")]
        public async Task<IEnumerable<CategoriaDto>> Get(string restaurante)
        {
            List<CategoriaDto> lista;

            lista = await _unitOfWork.Categoria.ObtenerTodos(restaurante);

            return lista;
        }

        // POST api/<CategoriasController>
        [HttpPost("Restaurantes/{restaurante}")]
        public async Task<IActionResult> Post([FromBody] CategoriaDtoIn categoria, string restaurante)
        {
            string id;
            CategoriaDto categoriaDto;
            // if (categoria.Id == Guid.Empty)
            //     categoriaDto = await _unitOfWork.Categoria.ObtenerPorId(categoria.Id);
            // else
            //     categoriaDto = null;
            // if (categoriaDto is not null)
            // {
            //     return Ok(categoriaDto);
            // }
            id = await _unitOfWork.Categoria.AgregarAsync(categoria, restaurante);

            return Created($"/Categorias/{id}", new { Id = id });
        }

        /// <summary>
        /// ACtualizar categoria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="categoria"></param>
        /// <returns></returns>
        [HttpPut("{id}/Restaurantes/{restaurante}")]
        public async Task<IActionResult> Put(string restaurante, string id, [FromBody] CategoriaDtoIn categoria)
        {
            await _unitOfWork.Categoria.ActualizarAsync(restaurante, id, categoria);

            return Accepted();
        }
    }
}