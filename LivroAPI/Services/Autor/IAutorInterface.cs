using LivroAPI.Dto.Autor;
using LivroAPI.Models;

namespace LivroAPI.Services.Autor
{
    public interface IAutorInterface
    {
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autor);
        Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autor);
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int id);

    }
}
