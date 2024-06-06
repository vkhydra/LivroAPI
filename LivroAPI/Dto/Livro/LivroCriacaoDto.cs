using LivroAPI.Dto.Vinculo;
using LivroAPI.Models;

namespace LivroAPI.Dto.Livro
{
    public class LivroCriacaoDto
    {

        public string Titulo { get; set; }
        public AutorVinculoDto Autor { get; set; }
    }
}
