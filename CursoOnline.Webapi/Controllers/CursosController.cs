using System.Threading.Tasks;
using CursoOnline.Dominio.DTO;
using CursoOnline.Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CursoOnline.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CursosController : ControllerBase
    {
        private readonly IAdicionarCursoCommand _addCommand;

        public CursosController(IAdicionarCursoCommand addCommand)
        { 
            _addCommand = addCommand;
        }
        
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post(CursoDTO cursoDTO)
        {
            if (await _addCommand.Adicionar(cursoDTO))
            {
                return Created("", cursoDTO);
            }
            return BadRequest();            
        }

    }
}