using LivroAPI.Data;
using LivroAPI.Dto.Livro;
using LivroAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LivroAPI.Services.Livro
{
    public class LivroService : ILivroInterface
    {
        private readonly AppDbContext _context;

        public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<LivroModel>> BuscarLivroPorId(int id)
        {
            ResponseModel<LivroModel> resposta = new ResponseModel<LivroModel>();
            try
            {
                var livro = await _context.Livros.Include(a => a.Autor).FirstOrDefaultAsync(
                    livroDb => livroDb.Id == id);
                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                resposta.Dados = livro;
                resposta.Mensagem = "Livro encontrado com sucesso!";
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

        public async Task<ResponseModel<List<LivroModel>>> BuscarLivrosPorIdAutor(int id)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                // Buscar livros do autor pelo id do autor
                var livros = await _context.Livros
                    .Include(livro => livro.Autor)
                    .Where(livroDb => livroDb.Autor.Id == id)
                    .ToListAsync();
                if (livros == null)
                {
                    resposta.Mensagem = "Livros não encontrados!";
                    resposta.Status = false;
                    return resposta;
                }
                // Retorna os livros do autor
                resposta.Dados = livros;
                resposta.Mensagem = "Livros encontrados com sucesso!";
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

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroCriacaoDto livroDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var autor = await _context.Autores.FirstOrDefaultAsync(
                    autorDb => autorDb.Id == livroDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                var livro = new LivroModel()
                {
                    Titulo = livroDto.Titulo,
                    Autor = autor
                };
                // Adiciona o livro no banco de dados
                await _context.Livros.AddAsync(livro);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro criado com sucesso!";
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

        public async Task<ResponseModel<List<LivroModel>>> EditarLivro(LivroEdicaoDto livroDto)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(livroDb => livroDb.Id == livroDto.Id);
                var autor = await _context.Autores.FirstOrDefaultAsync(
                    autorDb => autorDb.Id == livroDto.Autor.Id);
                if (autor == null)
                {
                    resposta.Mensagem = "Autor não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                // Atualiza os dados do livro
                livro.Titulo = livroDto.Titulo;
                livro.Autor = autor;
                // Atualiza o livro no banco de dados
                _context.Livros.Update(livro);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro editado com sucesso!";
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

        public async Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int id)
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                var livro = await _context.Livros
                    .Include(a => a.Autor)
                    .FirstOrDefaultAsync(
                    livroDb => livroDb.Id == id);
                if (livro == null)
                {
                    resposta.Mensagem = "Livro não encontrado!";
                    resposta.Status = false;
                    return resposta;
                }
                _context.Remove(livro);
                await _context.SaveChangesAsync();
                resposta.Dados = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Mensagem = "Livro excluído com sucesso!";
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

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> resposta = new ResponseModel<List<LivroModel>>();
            try
            {
                // Busca todos os livros
                var livros = await _context.Livros.Include(a => a.Autor).ToListAsync();
                resposta.Dados = livros;
                resposta.Mensagem = "Livros encontrados com sucesso!";
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
