using LivroAPI.Data;
using LivroAPI.Dto.Autor;
using LivroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivroAPI.Services.Autor
{
    public class AutorService : IAutorInterface
    {
        private readonly AppDbContext _context;
        public AutorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(
                    autorDb => autorDb.Id == id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = autor;
                resposta.Mensagem = "Autor encontrado com sucesso!";
                resposta.Status = true;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> resposta = new ResponseModel<AutorModel>();
            try
            {
                // Busca o autor do livro pelo id do livro
                var livro = await _context.Livros
                    .Include(livro => livro.Autor)
                    .FirstOrDefaultAsync(livro => livro.Id == idLivro);

                if (livro == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                // Retorna o autor do livro
                resposta.Dados = livro.Autor;
                resposta.Mensagem = "Autor encontrado com sucesso!";
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorCriacaoDto autorDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                // Cria um novo autor
                var autor = new AutorModel()
                {
                    Nome = autorDto.Nome,
                    Sobrenome = autorDto.Sobrenome
                };
                // Adiciona o autor ao contexto
                _context.Add(autor);
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor criado com sucesso!";
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> EditarAutor(AutorEdicaoDto autorDto)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(
                    autorDb => autorDb.Id == autorDto.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                autor.Nome = autorDto.Nome;
                autor.Sobrenome = autorDto.Sobrenome;
                // Atualiza o autor no contexto
                _context.Update(autor);
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();

                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor editado com sucesso!";
                resposta.Status = true;
                return resposta;

            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int id)
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(
                    autorDb => autorDb.Id == id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                _context.Remove(autor);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Autores.ToListAsync();
                resposta.Mensagem = "Autor excluído com sucesso!";
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            ResponseModel<List<AutorModel>> resposta = new ResponseModel<List<AutorModel>>();
            try
            {
                // Busca todos os autores
                var autores = await _context.Autores.ToListAsync();
                resposta.Dados = autores;
                resposta.Mensagem = "Autores listados com sucesso!";
                resposta.Status = true;
                return resposta;
            }
            catch (Exception ex)
            {
                resposta.Mensagem = ex.Message;
                resposta.Status = false;
                return resposta;
            }
        }
    }
}
