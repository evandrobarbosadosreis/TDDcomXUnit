using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CursosController : ControllerBase
    {
        private readonly IArmazenadorDeCurso _armazenador;
        private readonly IBuscadorDeCurso _buscador;
        private readonly IRemovedorDeCurso _removedor;

        public CursosController(IArmazenadorDeCurso armazenador, IBuscadorDeCurso buscador, IRemovedorDeCurso removedor)
        {
            _armazenador = armazenador;
            _buscador    = buscador;
            _removedor   = removedor;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var curso = await _buscador.BuscarPorId(id);

            if (curso == null)
            {
                return NotFound();
            }
            return Ok(curso);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get()
        {
            var cursos = await _buscador.BuscarTodos();

            return Ok(cursos);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoDTO cursoDTO)
        {
            var sucesso = await _armazenador.Armazenar(cursoDTO);

            return sucesso
                ? Created("", cursoDTO)
                : BadRequest();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Put(CursoDTO cursoDTO)
        {
            var existe = await _buscador.RegistroExiste(cursoDTO.Id);

            if (!existe)
            {
                return NotFound();
            }

            var sucesso = await _armazenador.Armazenar(cursoDTO);

            if (sucesso)
            {
                return NoContent();
            }

            return sucesso
                ? NoContent()
                : BadRequest();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var registroExiste = await _buscador.RegistroExiste(id);

            if (!registroExiste)
            {
                return NotFound();
            }

            var sucesso = await _removedor.Remover(id);

            return sucesso
                ? NoContent()
                : BadRequest();
        }

    }
}