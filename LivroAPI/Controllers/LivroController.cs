using LivroAPI.Dto.Livro;
using LivroAPI.Models;
using LivroAPI.Services.Livro;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LivroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }
        [HttpGet("ListarLivros")]
        public async Task<ActionResult<ResponseModel<List<LivroModel>>>> ListarLivros()
        {
            var livros = await _livroInterface.ListarLivros();
            return Ok(livros);
        }
        [HttpGet("BuscarLivroPorId/{id}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorId(int id)
        {
            var livros = await _livroInterface.BuscarLivroPorId(id);
            return Ok(livros);
        }

        [HttpGet("BuscarLivroPorIdLivro/{idAutor}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> BuscarLivroPorIdAutor(int idAutor)
        {
            var livros = await _livroInterface.BuscarLivrosPorIdAutor(idAutor);
            return Ok(livros);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> CriarLivro(LivroCriacaoDto livroDto)
        {
            var livrosAdicionado = await _livroInterface.CriarLivro(livroDto);
            return Ok(livrosAdicionado);
        }

        [HttpPut("EditarLivro")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> EditarLivro(LivroEdicaoDto livroDto)
        {
            var livrosEditado = await _livroInterface.EditarLivro(livroDto);
            return Ok(livrosEditado);
        }

        [HttpDelete("ExcluirLivro/{idLivro}")]
        public async Task<ActionResult<ResponseModel<LivroModel>>> DeletarLivro(int idLivro)
        {
            var livros = await _livroInterface.ExcluirLivro(idLivro);
            return Ok(livros);
        }
    }
}
