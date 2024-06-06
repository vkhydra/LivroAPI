using LivroAPI.Dto.Livro;
using LivroAPI.Models;

namespace LivroAPI.Services.Livro
{
    public interface ILivroInterface
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int id);
        Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int id);
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livro);
        Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livro);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int id);
    }
}
